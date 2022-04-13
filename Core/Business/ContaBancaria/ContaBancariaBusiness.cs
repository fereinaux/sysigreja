using Core.Models.ContaBancaria;
using Core.Models.Eventos;
using Data.Entities;
using Data.Repository;
using System.Linq;
using Utils.Enums;

namespace Core.Business.ContaBancaria
{
    public class ContaBancariaBusiness : IContaBancariaBusiness
    {
        private readonly IGenericRepository<Data.Entities.ContaBancaria> contaBancariaRepository;        

        public ContaBancariaBusiness(IGenericRepository<Data.Entities.ContaBancaria> contaBancariaRepository)
        {
            this.contaBancariaRepository = contaBancariaRepository;            
        }

        public void DeleteContaBancaria(int id)
        {
            contaBancariaRepository.Delete(id);
            contaBancariaRepository.Save();
        }

        public Data.Entities.ContaBancaria GetContaBancariaById(int id)
        {
            return contaBancariaRepository.GetById(id);
        }

        public IQueryable<Data.Entities.ContaBancaria> GetContasBancarias()
        {
            return contaBancariaRepository.GetAll();
        }

        public void PostContaBancaria(PostContaBancariaModel model)
        {
            Data.Entities.ContaBancaria contaBancaria = null;

            if (model.Id > 0)
            {
                contaBancaria = contaBancariaRepository.GetById(model.Id);

                contaBancaria.Agencia = model.Agencia;
                contaBancaria.Conta = model.Conta;
                contaBancaria.CPF = model.CPF;
                contaBancaria.Nome = model.Nome;
                contaBancaria.Operacao = model.Operacao;
                contaBancaria.Banco = model.Banco;

                contaBancariaRepository.Update(contaBancaria);
            }
            else
            {
                contaBancaria = new Data.Entities.ContaBancaria
                {
                    Status = StatusEnum.Ativo,
                    Nome = model.Nome,
                    Conta = model.Conta,
                    Agencia = model.Agencia,
                    CPF = model.CPF,
                    Operacao = model.Operacao,
                    Banco = model.Banco
                };

                contaBancariaRepository.Insert(contaBancaria);
            }

            contaBancariaRepository.Save();
        }
    }
}
