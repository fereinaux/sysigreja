using Core.Models.ContaBancaria;
using Core.Models.Eventos;
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
