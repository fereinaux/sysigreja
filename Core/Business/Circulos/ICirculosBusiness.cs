using Core.Models.Circulos;
using Core.Models.Eventos;
using System.Collections.Generic;
using System.Linq;

namespace Core.Business.Circulos
{
    public interface ICirculosBusiness
    {
        IEnumerable<Utils.Extensions.EnumExtensions.EnumModel> GetCores(int eventoId);
        List<Data.Entities.Participante> GetParticipantesSemCirculo(int eventoId);
        IQueryable<Data.Entities.Circulo> GetCirculos();
        IQueryable<Data.Entities.CirculoParticipante> GetParticipantesByCirculos(int id);
        IQueryable<Data.Entities.CirculoParticipante> GetCirculosComParticipantes(int eventoId);
        Data.Entities.Circulo GetCirculoById(int id);
        void PostCirculo(PostCirculoModel model);
        void DeleteCirculo(int id);
        Data.Entities.Circulo GetNextCirculo(int eventoId);
        void DistribuirCirculos(int eventoId);
        void ChangeCirculo(int participanteId, int? destinoId);
    }
}
