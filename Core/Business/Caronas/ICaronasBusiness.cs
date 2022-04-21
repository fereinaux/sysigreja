using Core.Models.Carona;
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
        void DistribuirCarona(int eventoId);
        string ChangeCarona(int participanteId, int? destinoId);
    }
}
