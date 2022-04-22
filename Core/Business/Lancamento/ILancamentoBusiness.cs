using Core.Models.Lancamento;
<<<<<<< HEAD
=======
using Core.Models.MeioPagamento;
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
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
