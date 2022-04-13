using Core.Models.Quartos;
using Core.Models.Eventos;
using System.Collections.Generic;
using System.Linq;
using Utils.Enums;

namespace Core.Business.Quartos
{
    public interface IQuartosBusiness
    {
        List<Data.Entities.Participante> GetParticipantesSemQuarto(int eventoId);
        IQueryable<Data.Entities.Quarto> GetQuartos();
        IQueryable<Data.Entities.QuartoParticipante> GetParticipantesByQuartos(int id);
        IQueryable<Data.Entities.QuartoParticipante> GetQuartosComParticipantes(int eventoId);
        Data.Entities.Quarto GetQuartoById(int id);
        void PostQuarto(PostQuartoModel model);
        void DeleteQuarto(int id);
        Data.Entities.Quarto GetNextQuarto(int eventoId, SexoEnum sexo);
        void DistribuirQuartos(int eventoId);
        string ChangeQuarto(int participanteId, int? destinoId);
    }
}
