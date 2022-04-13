using Data.Entities.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Utils.Enums;

namespace Data.Entities
{
    public class Configuracao : UsuarioBase
    {
        [Key]
        public int Id { get; set; }
        public int? LogoId { get; set; }
        public Arquivo Logo { get; set; }
        public int? BackgroundId { get; set; }
        public Arquivo Background { get; set; }
        public string Titulo { get; set; }
        public string CorBotao { get; set; }
        public string InscricaoConcluida { get; set; }
        public string InscricaoCompleta { get; set; }
        public string CorHoverBotao { get; set; }
        public string CorLoginBox { get; set; }
        public string CorScroll { get; set; }
        public string CorHoverScroll { get; set; }
        public TipoCirculoEnum TipoCirculo { get; set; }
    }
}
