using Core.Models.ContaBancaria;
<<<<<<< HEAD
=======
using Core.Models.Eventos;
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
using System.Linq;

namespace Core.Business.ContaBancaria
{
    public interface IContaBancariaBusiness
    {
        IQueryable<Data.Entities.ContaBancaria> GetContasBancarias();
        Data.Entities.ContaBancaria GetContaBancariaById(int id);
        void PostContaBancaria(PostContaBancariaModel model);
        void DeleteContaBancaria(int id);
    }
}
