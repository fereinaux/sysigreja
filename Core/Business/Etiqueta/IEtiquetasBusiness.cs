using Core.Models.Arquivos;
using Core.Models.Etiquetas;
using Core.Models.Eventos;
using System.Linq;

namespace Core.Business.Etiquetas
{
    public interface IEtiquetasBusiness
    {
        IQueryable<Data.Entities.Etiqueta> GetEtiquetas();
        IQueryable<Data.Entities.Etiqueta> GetEtiquetasByParticipante(int participanteId);
        void PostEtiqueta(PostEtiquetaModel model);
        Data.Entities.Etiqueta GetEtiquetaById(int id);
        void DeleteEtiqueta(int id);
    }
}
