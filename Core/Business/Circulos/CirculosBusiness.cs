using Core.Models.Circulos;
using Core.Models.Eventos;
using Data.Entities;
using Data.Repository;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Utils.Enums;
using Utils.Extensions;
using Utils.Services;

namespace Core.Business.Circulos
{
    public class CirculosBusiness : ICirculosBusiness
    {
        private readonly IGenericRepository<Circulo> circuloRepository;
        private readonly IGenericRepository<CirculoParticipante> circuloParticipanteRepository;
        private readonly IGenericRepository<Participante> participanteRepository;
        private readonly IGenericRepository<Evento> eventoeRepository;

        public CirculosBusiness(IGenericRepository<Evento> eventoeRepository, IGenericRepository<Participante> participanteRepository, IGenericRepository<Circulo> circuloRepository, IGenericRepository<CirculoParticipante> circuloParticipanteRepository)
        {
            this.circuloRepository = circuloRepository;
            this.participanteRepository = participanteRepository;
            this.circuloParticipanteRepository = circuloParticipanteRepository;
            this.eventoeRepository = eventoeRepository;
        }

        public void ChangeCirculo(int participanteId, int? destinoId)
        {
            var circuloParticipante = circuloParticipanteRepository.GetAll(x => x.ParticipanteId == participanteId).FirstOrDefault();

            if (destinoId.HasValue)
            {
                if (circuloParticipante == null)
                    circuloParticipante = new CirculoParticipante
                    {
                        ParticipanteId = participanteId
                    };

                circuloParticipante.CirculoId = destinoId.Value;
                circuloParticipanteRepository.InsertOrUpdate(circuloParticipante);
            }
            else
            {
                if (circuloParticipante != null)
                {
                    circuloParticipanteRepository.Delete(circuloParticipante.Id);
                }
            }

            circuloParticipanteRepository.Save();
        }

        public void DeleteCirculo(int id)
        {
            circuloRepository.Delete(id);
            circuloRepository.Save();
        }

        public void DistribuirCirculos(int eventoId)
        {
            List<Participante> listParticipantes = GetParticipantesSemCirculo(eventoId);

            foreach (var participante in listParticipantes)
            {
                circuloParticipanteRepository.Insert(
                    new CirculoParticipante
                    {
                        ParticipanteId = participante.Id,
                        CirculoId = GetNextCirculo(eventoId).Id
                    });
                circuloParticipanteRepository.Save();
            }
        }

        public Circulo GetCirculoById(int id)
        {
            return circuloRepository.GetAll(x => x.Id == id).Include(x => x.Dirigente1).Include(x => x.Dirigente1.Equipante)
                .Include(x => x.Dirigente2).Include(x => x.Dirigente2.Equipante).FirstOrDefault();
        }

        public IQueryable<Circulo> GetCirculos()
        {
            return circuloRepository.GetAll()
                .Include(x => x.Dirigente1)
                .Include(x => x.Dirigente1.Equipante)
                .Include(x => x.Dirigente2)
                .Include(x => x.Dirigente2.Equipante);
        }

        public IQueryable<CirculoParticipante> GetCirculosComParticipantes(int eventoId)
        {
            return circuloParticipanteRepository.GetAll(x => x.Circulo.EventoId == eventoId)
                .Include(x => x.Participante)
                .Include(x => x.Circulo)
                .Include(x => x.Circulo.Dirigente1)
                .Include(x => x.Circulo.Dirigente1.Equipante)
                .Include(x => x.Circulo.Dirigente2)
                .Include(x => x.Circulo.Dirigente2.Equipante).OrderBy(x => x.CirculoId);
        }

        public IEnumerable<EnumExtensions.EnumModel> GetCores(int eventoId)
        {
            return EnumExtensions.GetDescriptions<CoresEnum>();
        }

        public Circulo GetNextCirculo(int eventoId)
        {
            var query = circuloRepository
                .GetAll(x => x.EventoId == eventoId)
                .ToList()
                .Select(x => new
                {
                    Circulo = x,
                    Qtd = GetParticipantesByCirculos(x.Id).Count()
                })
                .ToList();

            return query
                .OrderBy(x => x.Qtd)
                .FirstOrDefault()?.Circulo;
        }

        public IQueryable<CirculoParticipante> GetParticipantesByCirculos(int id)
        {
            return circuloParticipanteRepository.GetAll(x => x.CirculoId == id).Include(x => x.Participante).Include(x => x.Participante.Arquivos).Include(x => x.Circulo);
        }

        public List<Participante> GetParticipantesSemCirculo(int eventoId)
        {
            var listParticipantesId = circuloParticipanteRepository
                           .GetAll(x => x.Circulo.EventoId == eventoId && x.Participante.Status != StatusEnum.Cancelado && x.Participante.Status != StatusEnum.Espera)
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

        public void PostCirculo(PostCirculoModel model)
        {
            Circulo circulo = null;
            var evento = eventoeRepository.GetById(model.EventoId);

            if (model.Id > 0)
            {
                circulo = circuloRepository.GetById(model.Id);
                circulo.Cor = model.Cor;

                circulo.Dirigente1Id = model.Dirigente1Id;

            }
            else
                circulo = new Circulo
                {
                    Dirigente1Id = model.Dirigente1Id,
                    EventoId = model.EventoId,
                    Cor = model.Cor
                };


            circuloRepository.InsertOrUpdate(circulo);
            circuloRepository.Save();
        }
    }
}
