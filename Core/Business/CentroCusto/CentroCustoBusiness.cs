using Core.Models.CentroCusto;
using Core.Models.Eventos;
using Data.Entities;
using Data.Repository;
using System.Linq;
using Utils.Enums;

namespace Core.Business.CentroCusto
{
    public class CentroCustoBusiness : ICentroCustoBusiness
    {
        private readonly IGenericRepository<Data.Entities.CentroCusto> centroCustoRepository;        

        public CentroCustoBusiness(IGenericRepository<Data.Entities.CentroCusto> centroCustoRepository)
        {
            this.centroCustoRepository = centroCustoRepository;            
        }

        public void DeleteCentroCusto(int id)
        {
            centroCustoRepository.Delete(id);
            centroCustoRepository.Save();
        }

        public Data.Entities.CentroCusto GetCentroCustoById(int id)
        {
            return centroCustoRepository.GetById(id);
        }

        public IQueryable<Data.Entities.CentroCusto> GetCentroCustos()
        {
            return centroCustoRepository.GetAll();
        }

        public void PostCentroCusto(PostCentroCustoModel model)
        {
            Data.Entities.CentroCusto centroCusto = null;

            if (model.Id > 0)
            {
                centroCusto = centroCustoRepository.GetById(model.Id);

                centroCusto.Descricao = model.Descricao;
                centroCusto.Tipo = model.Tipo;

                centroCustoRepository.Update(centroCusto);
            }
            else
            {
                centroCusto = new Data.Entities.CentroCusto
                {
                    Descricao = model.Descricao,
                    Tipo = model.Tipo
                };

                centroCustoRepository.Insert(centroCusto);
            }

            centroCustoRepository.Save();
        }
    }
}
