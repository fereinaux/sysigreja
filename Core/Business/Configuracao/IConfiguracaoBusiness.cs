using Core.Models.Configuracao;
using System.Collections.Generic;

namespace Core.Business.Configuracao
{
    public interface IConfiguracaoBusiness
    {
        PostConfiguracaoModel GetConfiguracao();
        IEnumerable<CamposModel> GetCampos();
        void PostCampos(IEnumerable<CamposModel> campos);
        void PostConfiguracao(PostConfiguracaoModel model);
        void PostLogo(int logoId);
        void PostBackground(int backgroundId);
    }
}
