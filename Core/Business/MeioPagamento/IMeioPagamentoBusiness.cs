using Core.Models.MeioPagamento;
using System.Linq;

namespace Core.Business.MeioPagamento
{
    public interface IMeioPagamentoBusiness
    {
        IQueryable<Data.Entities.MeioPagamento> GetMeioPagamentos();
        IQueryable<Data.Entities.MeioPagamento> GetAllMeioPagamentos();
        Data.Entities.MeioPagamento GetMeioPagamentoById(int id);
        void PostMeioPagamento(PostMeioPagamentoModel model);
        void DeleteMeioPagamento(int id);
    }
}
