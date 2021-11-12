using Core.Models.CentroCusto;
using Core.Models.Eventos;
using System.Linq;

namespace Core.Business.CentroCusto
{
    public interface ICentroCustoBusiness
    {
        IQueryable<Data.Entities.CentroCusto> GetCentroCustos();
        Data.Entities.CentroCusto GetCentroCustoById(int id);
        void PostCentroCusto(PostCentroCustoModel model);
        void DeleteCentroCusto(int id);
    }
}
