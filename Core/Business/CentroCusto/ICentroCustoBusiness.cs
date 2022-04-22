using Core.Models.CentroCusto;
<<<<<<< HEAD
=======
using Core.Models.Eventos;
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
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
