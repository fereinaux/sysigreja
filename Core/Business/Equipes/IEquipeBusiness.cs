using Core.Models.Equipe;
using Data.Entities;
using System.Collections.Generic;
using System.Linq;
using Utils.Enums;

namespace Core.Business.Equipes
{
    public interface IEquipesBusiness
    {
        IEnumerable<Utils.Extensions.EnumExtensions.EnumModel> GetEquipes(int? eventoId = null);
        IQueryable<EquipanteEvento> GetMembrosEquipe(int eventoId, EquipesEnum equipeId);
        EquipanteEvento GetEquipanteEventoByUser(int eventoId, string userId);
        List<Data.Entities.Equipante> GetEquipantesEventoSemEquipe(int eventoId);
        List<Data.Entities.Equipante> GetEquipantesByEvento(int eventoId);
        List<Data.Entities.EquipanteEvento> GetEquipantesEvento(int eventoId);
        List<Data.Entities.Equipante> GetEquipantesAniversariantesByEvento(int eventoId);
        List<Data.Entities.Equipante> GetEquipantesRestricoesByEvento(int eventoId);

        void AddMembroEquipe(PostEquipeMembroModel model);
        void ToggleMembroEquipeTipo(int id);
        void DeleteMembroEquipe(int id);
        EquipanteEvento GetEquipeAtual(int eventoId, int equipeId);
        IQueryable<PresencaReuniao> GetPresenca(int reuniaoId);
        void TogglePresenca(int equipanteEventoId, int reuniaoId);
    }
}
