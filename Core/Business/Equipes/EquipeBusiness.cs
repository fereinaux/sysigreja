using Core.Business.Eventos;
using Core.Models.Equipe;
using Data.Context;
using Data.Entities;
using Data.Repository;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Utils.Enums;
using static Utils.Extensions.EnumExtensions;
using System;

namespace Core.Business.Equipes
{
    public class EquipesBusiness : IEquipesBusiness
    {
        private readonly IGenericRepository<EquipanteEvento> equipanteEventoRepository;
        private readonly IGenericRepository<Equipante> equipanteRepository;
        private readonly IGenericRepository<PresencaReuniao> presencaRepository;
        private readonly IGenericRepository<ApplicationUser> usuarioRepository;
        private readonly IGenericRepository<Evento> eventoRepository;
        private readonly IEventosBusiness eventosBusiness;
        public EquipesBusiness(IEventosBusiness eventosBusiness, IGenericRepository<EquipanteEvento> equipanteEventoRepository, IGenericRepository<Evento> eventoRepository, IGenericRepository<Equipante> equipanteRepository, IGenericRepository<ApplicationUser> usuarioRepository, IGenericRepository<PresencaReuniao> presencaRepository)
        {
            this.eventosBusiness = eventosBusiness;
            this.equipanteEventoRepository = equipanteEventoRepository;
            this.equipanteRepository = equipanteRepository;
            this.eventoRepository = eventoRepository;
            this.usuarioRepository = usuarioRepository;
            this.presencaRepository = presencaRepository;
        }

        public void AddMembroEquipe(PostEquipeMembroModel model)
        {
            EquipanteEvento equipanteEvento = new EquipanteEvento
            {
                EquipanteId = model.EquipanteId,
                EventoId = model.EventoId,
                Tipo = TiposEquipeEnum.Membro,
                Equipe = model.Equipe
            };

            equipanteEventoRepository.Insert(equipanteEvento);
            equipanteEventoRepository.Save();
        }

        public void DeleteMembroEquipe(int id)
        {
            equipanteEventoRepository.Delete(id);
            equipanteEventoRepository.Save();
        }

        public EquipanteEvento GetEquipanteEventoByUser(int eventoId, string userId)
        {
            return equipanteEventoRepository
                .GetAll()
                .Include(x => x.Equipante)
                .Include(x => x.Evento)
                .ToList()
                .FirstOrDefault(x => x.EventoId == eventoId && x.EquipanteId == usuarioRepository.GetById(userId).EquipanteId);
        }

        public List<Equipante> GetEquipantesEventoSemEquipe(int eventoId)
        {
            var equipantesIds = equipanteEventoRepository.GetAll().Where(x => x.EventoId == eventoId).Select(y => y.EquipanteId).ToList();

            return equipanteRepository
                .GetAll()
                .ToList()
                .Where(x => !equipantesIds.Contains(x.Id)).ToList();
        }

        public List<Equipante> GetEquipantesByEvento(int eventoId)
        {
            return equipanteEventoRepository
                .GetAll()
                .Include(x => x.Equipante)
                .Include(x => x.Equipante.Arquivos)
                .Include(x => x.Equipe)
                .Where(x => x.EventoId == eventoId)
                .Select(y => y.Equipante).ToList();
        }

        public EquipanteEvento GetEquipeAtual(int eventoId, int equipanteId)
        {
            return equipanteEventoRepository.GetAll(x => x.EquipanteId == equipanteId && x.EventoId == eventoId).FirstOrDefault();
        }

        public IEnumerable<EnumModel> GetEquipes(int? eventoId = null)
        {
            try
            {
                ApplicationUser user = usuarioRepository.GetById(Thread.CurrentPrincipal.Identity.GetUserId());

                var equipanteEvento = equipanteEventoRepository.GetAll(x => x.EquipanteId == user.EquipanteId && x.EventoId == eventoId).FirstOrDefault();

                if (eventoId != null && user.Equipante != null && user.Perfil == PerfisUsuarioEnum.Coordenador && equipanteEvento != null && equipanteEvento.Tipo == TiposEquipeEnum.Coordenador)
                    return GetDescriptions<EquipesEnum>().Where(x => x.Description == equipanteEvento.Equipe.GetDescription());

                return GetEquipesEvento(eventoId);
            }
            catch (Exception)
            {
                return GetEquipesEvento(eventoId);
            }
        }

        private IEnumerable<EnumModel> GetEquipesEvento(int? eventoId)
        {
            var evento = eventoRepository.GetById(eventoId);

            return GetDescriptions<EquipesEnum>().Where(x => evento.TipoEvento.GetEquipes().Contains(x.Id));
        }

        public IQueryable<EquipanteEvento> GetMembrosEquipe(int eventoId, EquipesEnum equipeId)
        {
            return equipanteEventoRepository
                .GetAll(x => x.Equipe == equipeId && x.EventoId == eventoId)
                .Include(x => x.Equipante.Arquivos)
                .OrderBy(x => new { x.Tipo, x.Equipante.Nome });
        }

        public IQueryable<PresencaReuniao> GetPresenca(int reuniaoId)
        {
            return presencaRepository.GetAll(x => x.ReuniaoId == reuniaoId);
        }

        public void ToggleMembroEquipeTipo(int id)
        {
            EquipanteEvento equipanteEvento = equipanteEventoRepository.GetById(id);

            equipanteEvento.Tipo = equipanteEvento.Tipo == TiposEquipeEnum.Coordenador ?
                TiposEquipeEnum.Membro :
                TiposEquipeEnum.Coordenador;

            equipanteEventoRepository.Update(equipanteEvento);
            equipanteEventoRepository.Save();
        }

        public void TogglePresenca(int equipanteEventoId, int reuniaoId)
        {
            var presenca = presencaRepository.GetAll().FirstOrDefault(x => x.EquipanteEventoId == equipanteEventoId && x.ReuniaoId == reuniaoId);

            if (presenca != null)
                presencaRepository.Delete(presenca.Id);
            else
            {
                var newPresenca = new PresencaReuniao
                {
                    ReuniaoId = reuniaoId,
                    EquipanteEventoId = equipanteEventoId
                };
                presencaRepository.Insert(newPresenca);
            }

            presencaRepository.Save();
        }

        public List<Equipante> GetEquipantesAniversariantesByEvento(int eventoId)
        {
            var data = eventosBusiness.GetEventoById(eventoId).DataEvento;

            return GetEquipantesByEvento(eventoId).Where(x => x.DataNascimento?.Month == data.Month).ToList();
        }

        public List<Equipante> GetEquipantesRestricoesByEvento(int eventoId)
        {
            return GetEquipantesByEvento(eventoId).Where(x => x.HasRestricaoAlimentar).ToList();
        }

        public List<EquipanteEvento> GetEquipantesEvento(int eventoId)
        {
            return equipanteEventoRepository
                .GetAll()
                .Include(x => x.Equipante)
                .Where(x => x.EventoId == eventoId).ToList();
        }
    }
}
