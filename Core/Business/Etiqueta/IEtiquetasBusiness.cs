using Core.Models.Etiquetas;
using System.Linq;

namespace Core.Business.Etiquetas
{
    public interface IEtiquetasBusiness
    {
        IQueryable<Data.Entities.Etiqueta> GetEtiquetas();
        IQueryable<Data.Entities.Etiqueta> GetEtiquetasByParticipante(int participanteId);
        IQueryable<Data.Entities.Etiqueta> GetEtiquetasByEquipante(int equipanteId, int eventoId);
        void PostEtiqueta(PostEtiquetaModel model);
        Data.Entities.Etiqueta GetEtiquetaById(int id);
        void DeleteEtiqueta(int id);
    }
}
