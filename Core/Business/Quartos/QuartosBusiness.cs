using Core.Models.Quartos;
using Data.Entities;
using Data.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using Utils.Enums;
using Utils.Services;

namespace Core.Business.Quartos
{
    public class QuartosBusiness : IQuartosBusiness
    {
        private readonly IGenericRepository<Quarto> quartoRepository;
        private readonly IGenericRepository<QuartoParticipante> quartoParticipanteRepository;
        private readonly IGenericRepository<Participante> participanteRepository;

        public QuartosBusiness(IGenericRepository<Participante> participanteRepository, IGenericRepository<Quarto> quartoRepository, IGenericRepository<QuartoParticipante> quartoParticipanteRepository)
        {
            this.quartoRepository = quartoRepository;
            this.participanteRepository = participanteRepository;
            this.quartoParticipanteRepository = quartoParticipanteRepository;
        }

        public string ChangeQuarto(int participanteId, int? destinoId)
        {
            var quartoParticipante = quartoParticipanteRepository.GetAll(x => x.ParticipanteId == participanteId).FirstOrDefault();
            var participante = participanteRepository.GetById(participanteId);
            string mensagem = "OK";
            if (destinoId.HasValue)
            {
                var quarto = quartoRepository.GetById(destinoId.Value);
                if (participante.Sexo != quarto.Sexo)
                {
                    mensagem = "O gênero do participante difere do gênero do quarto";
                }
                else if (quarto.Capacidade < quartoParticipanteRepository.GetAll(x => x.QuartoId == quarto.Id).Count() + 1)
                {
                    mensagem = "Capacidade do quarto excedida";
                }
                else
                {
                    if (quartoParticipante == null)
                        quartoParticipante = new QuartoParticipante
                        {
                            ParticipanteId = participanteId
                        };

                    quartoParticipante.QuartoId = destinoId.Value;
                    quartoParticipanteRepository.InsertOrUpdate(quartoParticipante);
                }
            }
            else
            {
                if (quartoParticipante != null)
                {
                    quartoParticipanteRepository.Delete(quartoParticipante.Id);
                }
            }

            quartoParticipanteRepository.Save();
            return mensagem;
        }

        public void DeleteQuarto(int id)
        {
            quartoRepository.Delete(id);
            quartoRepository.Save();
        }

        public void DistribuirQuartos(int eventoId)
        {
            List<Participante> listParticipantes = GetParticipantesSemQuarto(eventoId);

            foreach (var participante in listParticipantes)
            {
                var quarto = GetNextQuarto(eventoId, participante.Sexo);

                if (quarto != null)
                {
                    quartoParticipanteRepository.Insert(
                        new QuartoParticipante
                        {
                            ParticipanteId = participante.Id,
                            QuartoId = quarto.Id
                        });
                    quartoParticipanteRepository.Save();
                }
            }
        }

        public Quarto GetQuartoById(int id)
        {
            return quartoRepository.GetById(id);
        }

        public IQueryable<Quarto> GetQuartos()
        {
            return quartoRepository.GetAll();
        }

        public IQueryable<QuartoParticipante> GetQuartosComParticipantes(int eventoId)
        {
            return quartoParticipanteRepository.GetAll(x => x.Quarto.EventoId == eventoId)
                .Include(x => x.Participante)
                .Include(x => x.Quarto)
                .OrderBy(x => x.QuartoId);
        }

        public Quarto GetNextQuarto(int eventoId, SexoEnum sexo)
        {
            var query = quartoRepository
                .GetAll(x => x.EventoId == eventoId && x.Sexo == sexo)
                .ToList()
                .Select(x => new
                {
                    Quarto = x,
                    Qtd = GetParticipantesByQuartos(x.Id).Count()
                })
                .Where(x => x.Qtd < x.Quarto.Capacidade)
                .ToList();

            return query.Any() ?
                query.OrderByDescending(x => x.Qtd)
                .FirstOrDefault().Quarto
                : null;
        }

        public IQueryable<QuartoParticipante> GetParticipantesByQuartos(int id)
        {
            return quartoParticipanteRepository.GetAll(x => x.QuartoId == id).Include(x => x.Participante);
        }

        public List<Participante> GetParticipantesSemQuarto(int eventoId)
        {
            var listParticipantesId = quartoParticipanteRepository
                           .GetAll(x => x.Quarto.EventoId == eventoId && x.Participante.Status != StatusEnum.Cancelado && x.Participante.Status != StatusEnum.Espera)
                           .Select(x => x.ParticipanteId)
                           .ToList();

            var listParticipantes = participanteRepository
                 .GetAll(x => x.EventoId == eventoId && !listParticipantesId.Contains(x.Id) && x.Status != StatusEnum.Cancelado && x.Status != StatusEnum.Espera)
                 .OrderBy(x => x.DataCadastro)
                 .ToList();

            listParticipantes.ForEach(x => x.Nome = UtilServices.CapitalizarNome(x.Nome));
            listParticipantes.ForEach(x => x.Apelido = UtilServices.CapitalizarNome(x.Apelido));

            return listParticipantes;
        }

        public void PostQuarto(PostQuartoModel model)
        {
            Quarto quarto = null;

            if (model.Id > 0)
            {
                quarto = quartoRepository.GetById(model.Id);
                quarto.Sexo = model.Sexo;
                quarto.Capacidade = model.Capacidade;
                quarto.Titulo = model.Titulo;

                quartoRepository.Update(quarto);
            }
            else
            {
                quarto = new Quarto
                {
                    Titulo = model.Titulo,
                    EventoId = model.EventoId,
                    Sexo = model.Sexo,
                    Capacidade = model.Capacidade
                };

                quartoRepository.Insert(quarto);
            }

            quartoRepository.Save();
        }
    }
}
