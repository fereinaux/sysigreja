using Core.Models.Arquivos;
using Core.Models.Eventos;
using Data.Entities;
using Data.Repository;
using System.IO;
using System.Linq;
using System.Data.Entity;
using Utils.Enums;

namespace Core.Business.Arquivos
{
    public class ArquivosBusiness : IArquivosBusiness
    {
        private readonly IGenericRepository<Arquivo> arquivoRepository;

        public ArquivosBusiness(IGenericRepository<Arquivo> arquivoRepository)
        {
            this.arquivoRepository = arquivoRepository;
        }

        public void DeleteArquivo(int id)
        {
            arquivoRepository.Delete(id);
            arquivoRepository.Save();
        }

        public void DeleteFotoEquipante(int id)
        {            
            arquivoRepository.Delete(arquivoRepository.GetAll(x => x.EquipanteId == id && x.IsFoto).FirstOrDefault().Id);
            arquivoRepository.Save();
        }

        public void DeleteFotoParticipante(int id)
        {
            arquivoRepository.Delete(arquivoRepository.GetAll(x => x.ParticipanteId == id && x.IsFoto).FirstOrDefault().Id);
            arquivoRepository.Save();
        }

        public Arquivo GetArquivoById(int id)
        {
            return arquivoRepository.GetById(id);
        }

        public IQueryable<Arquivo> GetArquivos()
        {
            return arquivoRepository.GetAll(x => x.EventoId == null && x.LancamentoId == null && x.ParticipanteId == null && x.EquipanteId == null);
        }

        public IQueryable<Arquivo> GetArquivosByEquipante(int equipanteId)
        {
            return arquivoRepository.GetAll(x => x.EquipanteId == equipanteId);
        }

        public IQueryable<Arquivo> GetArquivosByEquipanteEvento(int equipanteId, int eventoId)
        {
            return arquivoRepository.GetAll(x => x.EquipanteId == equipanteId && x.EventoId == eventoId);
        }

        public IQueryable<Arquivo> GetArquivosByEvento(int eventoId)
        {
            return arquivoRepository.GetAll(x => x.EventoId == eventoId && !x.EquipanteId.HasValue );
        }

        public IQueryable<Arquivo> GetArquivosByLancamento(int lancamentoId)
        {
            return arquivoRepository.GetAll(x => x.LancamentoId == lancamentoId);
        }

        public IQueryable<Arquivo> GetArquivosByParticipante(int participanteId)
        {
            return arquivoRepository.GetAll(x => x.ParticipanteId == participanteId);
        }

        public int PostArquivo(PostArquivoModel model)
        {
            using (var binaryReader = new BinaryReader(model.Arquivo.InputStream))
            {
                Arquivo arquivo = new Arquivo
                {
                    Conteudo = binaryReader.ReadBytes(model.Arquivo.ContentLength),
                    Tipo = model.Arquivo.ContentType,
                    Nome = model.Arquivo.FileName,
                    Extensao = Path.GetExtension(model.Arquivo.FileName).Replace(".", "").ToUpper(),
                    EventoId = model.EventoId,
                    ParticipanteId = model.ParticipanteId,
                    EquipanteId = model.EquipanteId,
                    LancamentoId = model.LancamentoId,
                    IsFoto = model.IsFoto
                };

                arquivoRepository.Insert(arquivo);
                arquivoRepository.Save();
                return arquivo.Id;
            }
        }

        public void SetEquipante(int participanteId, int equipanteId)
        {
            var arquivos = arquivoRepository.GetAll().Where(x => x.ParticipanteId == participanteId).ToList();

            arquivos.ForEach(arquivo =>
            {
                arquivo.EquipanteId = equipanteId;
                arquivoRepository.Update(arquivo);
            });

            arquivoRepository.Save();
        }
    }
}
