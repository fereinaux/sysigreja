using Core.Business.Configuracao;
using Core.Models.Carona;
using Data.Entities;
using Data.Repository;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Spatial;
using System.Linq;
using Utils.Enums;
using Utils.Services;

namespace Core.Business.Caronas
{
    public class CaronasBusiness : ICaronasBusiness
    {
        private readonly IGenericRepository<Carona> caronaRepo;
        private readonly IGenericRepository<CaronaParticipante> caronaParticipanteRepository;
        private readonly IGenericRepository<Participante> participanteRepository;
        private readonly IGenericRepository<Evento> eventoeRepository;
        private readonly IConfiguracaoBusiness configuracaoBusiness;

        public CaronasBusiness(IGenericRepository<Evento> eventoeRepository, IConfiguracaoBusiness configuracaoBusiness, IGenericRepository<Participante> participanteRepository, IGenericRepository<Carona> caronaRepo, IGenericRepository<CaronaParticipante> caronaParticipanteRepository)
        {
            this.caronaRepo = caronaRepo;
            this.configuracaoBusiness = configuracaoBusiness;
            this.participanteRepository = participanteRepository;
            this.caronaParticipanteRepository = caronaParticipanteRepository;
            this.eventoeRepository = eventoeRepository;
        }

        public string ChangeCarona(int participanteId, int? destinoId)
        {
            var caronaParticipante = caronaParticipanteRepository.GetAll(x => x.ParticipanteId == participanteId).FirstOrDefault();
            string msg = "OK";
            if (destinoId.HasValue)
            {
                var carona = caronaRepo.GetById(destinoId.Value);
                if (carona.Capacidade < caronaParticipanteRepository.GetAll(x => x.CaronaId == carona.Id).Count() + 1)
                {
                    msg = "Capacidade da carona excedida";
                }
                if (caronaParticipante == null)
                    caronaParticipante = new CaronaParticipante
                    {
                        ParticipanteId = participanteId
                    };

                caronaParticipante.CaronaId = destinoId.Value;
                caronaParticipanteRepository.InsertOrUpdate(caronaParticipante);
            }
            else
            {
                if (caronaParticipante != null)
                {
                    caronaParticipanteRepository.Delete(caronaParticipante.Id);
                }
            }

            caronaParticipanteRepository.Save();
            return msg;
        }

        public void DeleteCarona(int id)
        {
            caronaRepo.Delete(id);
            caronaRepo.Save();
        }

        public void DistribuirCarona(int eventoId)
        {
            var config = configuracaoBusiness.GetConfiguracao();
            var caronas = caronaRepo.GetAll(x => x.EventoId == eventoId).Include(x => x.Motorista).ToList();
            List<Participante> listParticipantes = GetParticipantesSemCarona(eventoId);
            var countParticipantes = listParticipantes.Count;

            listParticipantes = listParticipantes.Where(x => !string.IsNullOrEmpty(x.CEP)).ToList();

            caronas.ForEach(carona =>
            {
                var pontoZero = DbGeography.FromText($"POINT({carona.Motorista.Latitude} {carona.Motorista.Longitude})");
                listParticipantes = listParticipantes.OrderBy(x => pontoZero.Distance(DbGeography.FromText($"POINT({x.Latitude} {x.Longitude})"))).ToList();
                while (caronaParticipanteRepository.GetAll(x => x.CaronaId == carona.Id).Count() < carona.Capacidade)
                {
                    caronaParticipanteRepository.Insert(
                          new CaronaParticipante
                          {
                              ParticipanteId = listParticipantes[0].Id,
                              CaronaId = carona.Id
                          });
                    caronaParticipanteRepository.Save();
                    listParticipantes.RemoveAt(0);
                }

            });
        }

        public Carona GetCaronaById(int id)
        {
            return caronaRepo.GetAll(x => x.Id == id).Include(x => x.Motorista).FirstOrDefault();
        }

        public IQueryable<Carona> GetCaronas()
        {
            return caronaRepo.GetAll()
                .Include(x => x.Motorista);
        }

        public IQueryable<CaronaParticipante> GetCaronasComParticipantes(int eventoId)
        {
            return caronaParticipanteRepository.GetAll(x => x.Carona.EventoId == eventoId)
              .Include(x => x.Participante)
              .Include(x => x.Carona)
              .Include(x => x.Carona.Motorista)
              .OrderBy(x => x.CaronaId);
        }

        public IQueryable<CaronaParticipante> GetParticipantesByCaronas(int id)
        {
            return caronaParticipanteRepository.GetAll(x => x.CaronaId == id).Include(x => x.Participante);
        }

        public List<Participante> GetParticipantesSemCarona(int eventoId)
        {
            var listParticipantesId = caronaParticipanteRepository
                           .GetAll(x => x.Carona.EventoId == eventoId && x.Participante.Status != StatusEnum.Cancelado && x.Participante.Status != StatusEnum.Espera)
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

        public void PostCarona(PostCaronaModel model)
        {
            Carona carona = null;

            if (model.Id > 0)
            {
                carona = caronaRepo.GetById(model.Id);
                carona.Capacidade = model.Capacidade;
                carona.MotoristaId = model.MotoristaId;

                caronaRepo.Update(carona);
            }
            else
            {
                carona = new Carona
                {
                    EventoId = model.EventoId,
                    Capacidade = model.Capacidade,
                    MotoristaId = model.MotoristaId
                };

                caronaRepo.Insert(carona);
            }

            caronaRepo.Save();
        }
    }
}
