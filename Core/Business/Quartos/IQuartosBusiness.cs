using Core.Models.Quartos;
using System.Collections.Generic;
using System.Linq;
using Utils.Enums;

namespace Core.Business.Quartos
{
    public interface IQuartosBusiness
    {
        List<Data.Entities.Participante> GetParticipantesSemQuarto(int eventoId);
        List<Data.Entities.Equipante> GetEquipantesSemQuarto(int eventoId);
        IQueryable<Data.Entities.Quarto> GetQuartos();
        IQueryable<Data.Entities.QuartoParticipante> GetParticipantesByQuartos(int id, TipoPessoaEnum? tipo);
        IQueryable<Data.Entities.QuartoParticipante> GetQuartosComParticipantes(int eventoId, TipoPessoaEnum? tipo);
        Data.Entities.Quarto GetQuartoById(int id);
        void PostQuarto(PostQuartoModel model);
        void DeleteQuarto(int id);
        Data.Entities.Quarto GetNextQuarto(int eventoId, SexoEnum sexo, TipoPessoaEnum? tipo);
        void DistribuirQuartos(int eventoId, TipoPessoaEnum? tipo);
        string ChangeQuarto(int participanteId, int? destinoId, TipoPessoaEnum? tipo);
    }
}
