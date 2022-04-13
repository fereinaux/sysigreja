using Core.Models.CentroCusto;
using Core.Models.Eventos;
using Core.Models.Mensagem;
using Data.Entities;
using Data.Repository;
using System.Linq;
using Utils.Enums;

namespace Core.Business.Mensagem
{
    public class MenssagemBusinesss : IMensagemBusiness
    {
        private readonly IGenericRepository<Data.Entities.Mensagem> repo;        

        public MenssagemBusinesss(IGenericRepository<Data.Entities.Mensagem> repo)
        {
            this.repo = repo;            
        }



        public void DeleteMensagem(int id)
        {
            repo.Delete(id);
            repo.Save();
        }

        public Data.Entities.Mensagem GetMensagemById(int id)
        {
            return repo.GetById(id);
        }

        public IQueryable<Data.Entities.Mensagem> GetMensagems()
        {
            return repo.GetAll();
        }

        public void PostMensagem(PostMessageModel model)
        {
            Data.Entities.Mensagem mensagem = null;

            if (model.Id > 0)
            {
                mensagem = repo.GetById(model.Id);

                mensagem.Conteudo = model.Conteudo;
                mensagem.Titulo = model.Titulo;

                repo.Update(mensagem);
            }
            else
            {
                mensagem = new Data.Entities.Mensagem
                {
                    Conteudo = model.Conteudo,
                    Titulo = model.Titulo
                };

                repo.Insert(mensagem);
            }

            repo.Save();
        }

   
    }
}
