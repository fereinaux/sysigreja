using Core.Models.Quartos;
using Data.Entities;
using Data.Repository;
using System.Collections.Generic;
<<<<<<< HEAD
using System.Data.Entity;
using System.Linq;
=======
using System.Linq;
using System.Data.Entity;
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
using Utils.Enums;
using Utils.Services;

namespace Core.Business.Quartos
{
    public class QuartosBusiness : IQuartosBusiness
    {
        private readonly IGenericRepository<Quarto> quartoRepository;
        private readonly IGenericRepository<QuartoParticipante> quartoParticipanteRepository;
        private readonly IGenericRepository<Participante> participanteRepository;
<<<<<<< HEAD
        private readonly IGenericRepository<Equipante> equipanteRepository;

        public QuartosBusiness(IGenericRepository<Participante> participanteRepository, IGenericRepository<Equipante> equipanteRepository, IGenericRepository<Quarto> quartoRepository, IGenericRepository<QuartoParticipante> quartoParticipanteRepository)
=======

        public QuartosBusiness(IGenericRepository<Participante> participanteRepository, IGenericRepository<Quarto> quartoRepository, IGenericRepository<QuartoParticipante> quartoParticipanteRepository)
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
        {
            this.quartoRepository = quartoRepository;
            this.participanteRepository = participanteRepository;
            this.quartoParticipanteRepository = quartoParticipanteRepository;
<<<<<<< HEAD
            this.equipanteRepository = equipanteRepository;
        }

        public string ChangeQuarto(int participanteId, int? destinoId, TipoPessoaEnum? tipo)
        {
            if (tipo == TipoPessoaEnum.Equipante)
            {
                var quartoParticipante = quartoParticipanteRepository.GetAll(x => x.EquipanteId == participanteId).FirstOrDefault();
                var participante = equipanteRepository.GetById(participanteId);
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
                                EquipanteId = participanteId
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
            else
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

=======
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
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
        }

        public void DeleteQuarto(int id)
        {
            quartoRepository.Delete(id);
            quartoRepository.Save();
        }

<<<<<<< HEAD
        public void DistribuirQuartos(int eventoId, TipoPessoaEnum? tipo)
=======
        public void DistribuirQuartos(int eventoId)
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
        {
            List<Participante> listParticipantes = GetParticipantesSemQuarto(eventoId);

            foreach (var participante in listParticipantes)
            {
<<<<<<< HEAD
                var quarto = GetNextQuarto(eventoId, participante.Sexo, tipo);
=======
                var quarto = GetNextQuarto(eventoId, participante.Sexo);
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566

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

<<<<<<< HEAD
        public IQueryable<QuartoParticipante> GetQuartosComParticipantes(int eventoId, TipoPessoaEnum? tipo)
        {
            return quartoParticipanteRepository.GetAll(x => x.Quarto.EventoId == eventoId && x.Quarto.TipoPessoa == (tipo ?? TipoPessoaEnum.Participante))
                .Include(x => x.Participante)
                .Include(x => x.Equipante)
=======
        public IQueryable<QuartoParticipante> GetQuartosComParticipantes(int eventoId)
        {
            return quartoParticipanteRepository.GetAll(x => x.Quarto.EventoId == eventoId)
                .Include(x => x.Participante)
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
                .Include(x => x.Quarto)
                .OrderBy(x => x.QuartoId);
        }

<<<<<<< HEAD
        public Quarto GetNextQuarto(int eventoId, SexoEnum sexo, TipoPessoaEnum? tipo)
        {
            var query = quartoRepository
                .GetAll(x => x.EventoId == eventoId && x.Sexo == sexo && x.TipoPessoa == (tipo ?? TipoPessoaEnum.Participante))
=======
        public Quarto GetNextQuarto(int eventoId, SexoEnum sexo)
        {
            var query = quartoRepository
                .GetAll(x => x.EventoId == eventoId && x.Sexo == sexo)
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
                .ToList()
                .Select(x => new
                {
                    Quarto = x,
<<<<<<< HEAD
                    Qtd = GetParticipantesByQuartos(x.Id, tipo).Count()
=======
                    Qtd = GetParticipantesByQuartos(x.Id).Count()
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
                })
                .Where(x => x.Qtd < x.Quarto.Capacidade)
                .ToList();

            return query.Any() ?
                query.OrderByDescending(x => x.Qtd)
                .FirstOrDefault().Quarto
                : null;
        }

<<<<<<< HEAD
        public IQueryable<QuartoParticipante> GetParticipantesByQuartos(int id, TipoPessoaEnum? tipo)
        {
            return quartoParticipanteRepository.GetAll(x => x.QuartoId == id && x.Quarto.TipoPessoa == (tipo ?? TipoPessoaEnum.Participante)).Include(x => x.Participante).Include(x => x.Equipante);
=======
        public IQueryable<QuartoParticipante> GetParticipantesByQuartos(int id)
        {
            return quartoParticipanteRepository.GetAll(x => x.QuartoId == id).Include(x => x.Participante);
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
        }

        public List<Participante> GetParticipantesSemQuarto(int eventoId)
        {
            var listParticipantesId = quartoParticipanteRepository
<<<<<<< HEAD
                           .GetAll(x => x.Quarto.EventoId == eventoId && x.Participante.Status == StatusEnum.Confirmado && x.Quarto.TipoPessoa == TipoPessoaEnum.Participante)
=======
                           .GetAll(x => x.Quarto.EventoId == eventoId && x.Participante.Status != StatusEnum.Cancelado && x.Participante.Status != StatusEnum.Espera)
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
                           .Select(x => x.ParticipanteId)
                           .ToList();

            var listParticipantes = participanteRepository
<<<<<<< HEAD
                 .GetAll(x => x.EventoId == eventoId && !listParticipantesId.Contains(x.Id) && x.Status == StatusEnum.Confirmado)
=======
                 .GetAll(x => x.EventoId == eventoId && !listParticipantesId.Contains(x.Id) && x.Status != StatusEnum.Cancelado && x.Status != StatusEnum.Espera)
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
                 .OrderBy(x => x.DataCadastro)
                 .ToList();

            listParticipantes.ForEach(x => x.Nome = UtilServices.CapitalizarNome(x.Nome));
            listParticipantes.ForEach(x => x.Apelido = UtilServices.CapitalizarNome(x.Apelido));

            return listParticipantes;
        }

<<<<<<< HEAD
        public List<Equipante> GetEquipantesSemQuarto(int eventoId)
        {
            var listParticipantesId = quartoParticipanteRepository
                           .GetAll(x => x.Quarto.EventoId == eventoId && x.Quarto.TipoPessoa == TipoPessoaEnum.Equipante)
                           .Include(x => x.Equipante)
                              .Include(x => x.Equipante.Equipes)
                           .ToList()
                           .Where(x => x.Equipante.Equipes.Any() && x.Equipante.Equipes.LastOrDefault()?.EventoId == eventoId)
                           .Select(x => x.EquipanteId)
                           .ToList();

            var listParticipantes = equipanteRepository
                 .GetAll(x => !listParticipantesId.Contains(x.Id))
                 .Include(x => x.Equipes)
                 .ToList()
                 .Where(x => x.Equipes.Any() && x.Equipes.LastOrDefault()?.EventoId == eventoId)
                 .OrderBy(x => x.Nome)
                 .ToList();

            listParticipantes.ForEach(x => x.Nome = UtilServices.CapitalizarNome(x.Nome));
            listParticipantes.ForEach(x => x.Apelido = UtilServices.CapitalizarNome(x.Apelido));

            return listParticipantes;
        }

=======
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
        public void PostQuarto(PostQuartoModel model)
        {
            Quarto quarto = null;

            if (model.Id > 0)
            {
                quarto = quartoRepository.GetById(model.Id);
                quarto.Sexo = model.Sexo;
                quarto.Capacidade = model.Capacidade;
                quarto.Titulo = model.Titulo;

<<<<<<< HEAD

=======
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
                quartoRepository.Update(quarto);
            }
            else
            {
                quarto = new Quarto
                {
                    Titulo = model.Titulo,
                    EventoId = model.EventoId,
                    Sexo = model.Sexo,
<<<<<<< HEAD
                    Capacidade = model.Capacidade,
                    TipoPessoa = model.TipoPessoa ?? TipoPessoaEnum.Participante
=======
                    Capacidade = model.Capacidade
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
                };

                quartoRepository.Insert(quarto);
            }

            quartoRepository.Save();
        }
    }
}
