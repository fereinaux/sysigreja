using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models.MeioPagamento;
using Data.Entities;
using Data.Repository;
using Utils.Enums;

namespace Core.Business.MeioPagamento
{
    public class MeioPagamentoBusiness : IMeioPagamentoBusiness
    {
        private readonly IGenericRepository<Data.Entities.MeioPagamento> meioPagamentoRepository;

        public MeioPagamentoBusiness(IGenericRepository<Data.Entities.MeioPagamento> meioPagamentoRepository)
        {
            this.meioPagamentoRepository = meioPagamentoRepository;
        }

        public void DeleteMeioPagamento(int id)
        {
            meioPagamentoRepository.Delete(id);
            meioPagamentoRepository.Save();
        }

        public Data.Entities.MeioPagamento GetMeioPagamentoById(int id)
        {
            return meioPagamentoRepository.GetById(id);
        }

        public IQueryable<Data.Entities.MeioPagamento> GetMeioPagamentos()
        {
             return meioPagamentoRepository.GetAll(mp => mp.IsEditavel == true);
        }

        public IQueryable<Data.Entities.MeioPagamento> GetAllMeioPagamentos()
        {
            return meioPagamentoRepository.GetAll();
        }

        public void PostMeioPagamento(PostMeioPagamentoModel model)
        {
            Data.Entities.MeioPagamento meioPagamento = null;

            if (model.Id > 0)
            {
                meioPagamento = meioPagamentoRepository.GetById(model.Id);

                meioPagamento.Descricao = model.Descricao;
                meioPagamento.Taxa = model.Taxa;

                meioPagamentoRepository.Update(meioPagamento);
            }
            else
            {
                meioPagamento = new Data.Entities.MeioPagamento
                {
                    Descricao = model.Descricao,
                    Taxa = model.Taxa,
                    Status = StatusEnum.Ativo, 
                    IsEditavel = true
                };

                meioPagamentoRepository.Insert(meioPagamento);
            }

            meioPagamentoRepository.Save();
        }
    }
}
