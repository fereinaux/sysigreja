using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.Enums;

namespace Core.Models.Configuracao
{
    public class PostConfiguracaoModel
    {
        public int? LogoId { get; set; }
        public string Logo { get; set; }
        public int? BackgroundId { get; set; }
        public string Background { get; set; }
        public string Titulo { get; set; }
        public string CorBotao { get; set; }
        public string CorHoverBotao { get; set; }
        public string CorLoginBox { get; set; }
        public string CorScroll { get; set; }
        public string CorHoverScroll { get; set; }
        public TipoCirculoEnum TipoCirculoId { get; set; }
        public string TipoCirculo { get; set; }
    }

    public class CamposModel
    {
        public int Id { get; set; }
        public CamposEnum CampoId { get; set; }
        public string Campo { get; set; }
    }
}
