using Core.Models.Carona;
using Core.Models.Circulos;
using Core.Models.Eventos;
using System.Collections.Generic;
using System.Linq;

namespace Core.Business.Caronas
{
    public interface ICaronasBusiness
    {
        List<Data.Entities.Participante> GetParticipantesSemCarona(int eventoId);
        IQueryable<Data.Entities.Carona> GetCaronas();
        IQueryable<Data.Entities.CaronaParticipante> GetParticipantesByCaronas(int id);
        IQueryable<Data.Entities.CaronaParticipante> GetCaronasComParticipantes(int eventoId);
        Data.Entities.Carona GetCaronaById(int id);
        void PostCarona(PostCaronaModel model);
        void DeleteCarona(int id);
        Data.Entities.Carona GetNextCarona(int eventoId);
        void DistribuirCarona(int eventoId);
        void ChangeCarona(int participanteId, int? destinoId);
    }
}
