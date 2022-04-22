using Core.Models.Quartos;
<<<<<<< HEAD
=======
using Core.Models.Eventos;
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
using System.Collections.Generic;
using System.Linq;
using Utils.Enums;

namespace Core.Business.Quartos
{
    public interface IQuartosBusiness
    {
        List<Data.Entities.Participante> GetParticipantesSemQuarto(int eventoId);
<<<<<<< HEAD
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
=======
        IQueryable<Data.Entities.Quarto> GetQuartos();
        IQueryable<Data.Entities.QuartoParticipante> GetParticipantesByQuartos(int id);
        IQueryable<Data.Entities.QuartoParticipante> GetQuartosComParticipantes(int eventoId);
        Data.Entities.Quarto GetQuartoById(int id);
        void PostQuarto(PostQuartoModel model);
        void DeleteQuarto(int id);
        Data.Entities.Quarto GetNextQuarto(int eventoId, SexoEnum sexo);
        void DistribuirQuartos(int eventoId);
        string ChangeQuarto(int participanteId, int? destinoId);
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
    }
}
