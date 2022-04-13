using Core.Models.CentroCusto;
using Core.Models.Configuracao;
using Core.Models.Eventos;
using Data.Entities;
using Data.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Utils.Enums;
using Utils.Extensions;

namespace Core.Business.Configuracao
{
    public class ConfiguracaoBusiness : IConfiguracaoBusiness
    {
        private readonly IGenericRepository<Data.Entities.Configuracao> repo;
        private readonly IGenericRepository<ConfiguracaoCampos> camposRepo;

        public ConfiguracaoBusiness(IGenericRepository<Data.Entities.Configuracao> repo, IGenericRepository<ConfiguracaoCampos> camposRepo)
        {
            this.repo = repo;
            this.camposRepo = camposRepo;
        }

        public PostConfiguracaoModel GetConfiguracao()
        {
            return repo.GetAll().Include(x => x.Logo).Include(x => x.Background).ToList().Select(x => new PostConfiguracaoModel
            {
                Titulo = x.Titulo,
                BackgroundId = x.BackgroundId,
                CorBotao = x.CorBotao,
                CorHoverBotao = x.CorHoverBotao,
                CorHoverScroll = x.CorHoverScroll,
                TipoCirculoId= x.TipoCirculo,
                TipoCirculo = x.TipoCirculo.GetDescription(),
                CorLoginBox = x.CorLoginBox,
                CorScroll = x.CorScroll,
                LogoId = x.LogoId,
                Logo = x.Logo != null ? Convert.ToBase64String(x.Logo.Conteudo) : "",
                Background = x.Background != null ? Convert.ToBase64String(x.Background.Conteudo) : ""
                
            }).FirstOrDefault();
        }

        public IEnumerable<CamposModel> GetCampos()
        {
            return camposRepo.GetAll().ToList().Select(x => new CamposModel
            {
                Campo = x.Campo.GetDescription(),
                CampoId = x.Campo,
                Id = x.Id
            });
        }

        public void PostCampos(IEnumerable<CamposModel> campos)
        {
            var camposBanco = camposRepo.GetAll().ToList();

            camposBanco.ForEach(campoBanco =>
            {
                if (!campos.Select(x => x.CampoId).ToList().Any(y => y == campoBanco.Campo))
                {
                    camposRepo.Delete(campoBanco.Id);
                }
            });

            campos.ToList().ForEach(campo =>
            {
                if (!camposBanco.Select(x => x.Campo).ToList().Any(y => y == campo.CampoId))
                {
                    camposRepo.Insert(new Data.Entities.ConfiguracaoCampos
                    {
                        Campo = campo.CampoId
                    });
                }
            });

            camposRepo.Save();
        }

        public void PostBackground(int backgroundId)
        {
            Data.Entities.Configuracao configuracao = repo.GetAll().FirstOrDefault();
            configuracao.BackgroundId = backgroundId;
            repo.Save();
        }

        public void PostConfiguracao(PostConfiguracaoModel model)
        {
            Data.Entities.Configuracao configuracao = repo.GetAll().FirstOrDefault();

            configuracao.CorBotao = model.CorBotao;
            configuracao.TipoCirculo = model.TipoCirculoId;
            configuracao.CorHoverBotao = model.CorHoverBotao;
            configuracao.CorHoverScroll = model.CorHoverScroll;
            configuracao.CorScroll = model.CorScroll;
            configuracao.Titulo = model.Titulo;
            configuracao.CorLoginBox = model.CorLoginBox;
            repo.Update(configuracao);

            repo.Save();
        }

        public void PostLogo(int logoId)
        {
            Data.Entities.Configuracao configuracao = repo.GetAll().FirstOrDefault();
            configuracao.LogoId = logoId;
            repo.Save();
        }
    }
}
