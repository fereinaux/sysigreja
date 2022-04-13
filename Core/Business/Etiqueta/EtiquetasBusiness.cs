using Core.Models.Arquivos;
using Core.Models.Eventos;
using Data.Entities;
using Data.Repository;
using System.IO;
using System.Linq;
using System.Data.Entity;
using Utils.Enums;
using Core.Business.Etiquetas;
using Core.Models.Etiquetas;

namespace Core.Business.Etiquetas
{
    public class EtiquetasBusiness : IEtiquetasBusiness
    {
        private readonly IGenericRepository<Etiqueta> etiquetaRepo;
        private readonly IGenericRepository<ParticipantesEtiquetas> ParticipanteEtiquetasRepo;

        public EtiquetasBusiness(IGenericRepository<Etiqueta> etiquetaRepo, IGenericRepository<ParticipantesEtiquetas> ParticipanteEtiquetasRepo)
        {
            this.etiquetaRepo = etiquetaRepo;
            this.ParticipanteEtiquetasRepo = ParticipanteEtiquetasRepo;
        }

        public void DeleteEtiqueta(int id)
        {
            etiquetaRepo.Delete(id);
            etiquetaRepo.Save();
        }

        public Etiqueta GetEtiquetaById(int id)
        {
            return etiquetaRepo.GetById(id);
        }

        public IQueryable<Etiqueta> GetEtiquetas()
        {
           return etiquetaRepo.GetAll();
        }

        public IQueryable<Etiqueta> GetEtiquetasByParticipante(int participanteId)
        {
            return ParticipanteEtiquetasRepo.GetAll(x => x.ParticipanteId == participanteId)?.Include(x => x.Etiqueta)?.Select(x => x.Etiqueta);
        }

        public void PostEtiqueta(PostEtiquetaModel model)
        {
            Data.Entities.Etiqueta etiqueta = null;

            if (model.Id > 0)
            {
                etiqueta = etiquetaRepo.GetById(model.Id);

                etiqueta.Nome = model.Nome;
                etiqueta.Cor = model.Cor;

                etiquetaRepo.Update(etiqueta);
            }
            else
            {
                etiqueta = new Data.Entities.Etiqueta
                {
                    Nome = model.Nome,
                    Cor = model.Cor
                };

                etiquetaRepo.Insert(etiqueta);
            }

            etiquetaRepo.Save();
        }
    }
}
