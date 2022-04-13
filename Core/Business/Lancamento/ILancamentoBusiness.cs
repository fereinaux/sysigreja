using Core.Models.Lancamento;
using Core.Models.MeioPagamento;
using System.Linq;

namespace Core.Business.Lancamento
{
    public interface ILancamentoBusiness
    {
        void PostPagamento(PostPagamentoModel model);
        void PostLancamento(PostLancamentoModel model);
        void DeleteLancamento(int id);
        Data.Entities.Lancamento GetLancamentoById(int id);
        IQueryable<Data.Entities.Lancamento> GetPagamentosEvento(int eventoId);
        IQueryable<Data.Entities.Lancamento> GetLancamentos();
        IQueryable<Data.Entities.Lancamento> GetPagamentosParticipante(int participanteId);
        IQueryable<Data.Entities.Lancamento> GetPagamentosEquipante(int equipanteId);
    }
}
