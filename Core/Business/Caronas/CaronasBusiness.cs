using Core.Business.Configuracao;
using Core.Models.Carona;
using Core.Models.Circulos;
using Core.Models.Eventos;
using Data.Entities;
using Data.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Spatial;
using System.Linq;
using Utils.Enums;
using Utils.Extensions;
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

        public void ChangeCarona(int participanteId, int? destinoId)
        {
            var caronaParticipante = caronaParticipanteRepository.GetAll(x => x.ParticipanteId == participanteId).FirstOrDefault();

            if (destinoId.HasValue)
            {
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
            int countDistrbuir = (int)Math.Ceiling((decimal)(countParticipantes / caronas.Count()));
            var caronaAtual = 0;

            listParticipantes = listParticipantes.Where(x => !string.IsNullOrEmpty(x.CEP)).ToList();
            countParticipantes = listParticipantes.Count();
            countDistrbuir = (int)Math.Ceiling((decimal)(countParticipantes / caronas.Count()));
            var pontoZero = DbGeography.FromText($"POINT({caronas[caronaAtual].Motorista.Latitude} {caronas[caronaAtual].Motorista.Longitude})");
            while (listParticipantes.Count() > 0)
            {
                listParticipantes = listParticipantes.OrderBy(x => pontoZero.Distance(DbGeography.FromText($"POINT({x.Latitude} {x.Longitude})"))).ToList();
                for (int i = 0; i < countDistrbuir; i++)
                {
                    caronaParticipanteRepository.Insert(
                          new CaronaParticipante
                          {
                              ParticipanteId = listParticipantes[0].Id,
                              CaronaId = caronas[caronaAtual].Id
                          });
                    caronaParticipanteRepository.Save();
                    listParticipantes.RemoveAt(0);
                }
                caronaAtual++;
                pontoZero = DbGeography.FromText($"POINT({caronas[caronaAtual].Motorista.Latitude} {caronas[caronaAtual].Motorista.Longitude})");
            }

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

        public Carona GetNextCarona(int eventoId)
        {
            throw new NotImplementedException();
        }

        public IQueryable<CaronaParticipante> GetParticipantesByCaronas(int id)
        {
            throw new NotImplementedException();
        }

        public List<Participante> GetParticipantesSemCarona(int eventoId)
        {
            throw new NotImplementedException();
        }

        public void PostCarona(PostCaronaModel model)
        {
            throw new NotImplementedException();
        }
    }
}
