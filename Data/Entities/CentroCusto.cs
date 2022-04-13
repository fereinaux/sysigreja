using Data.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Utils.Enums;

namespace Data.Entities
{
    public class CentroCusto : UsuarioBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Descricao { get; set; }
        public TiposCentroCustoEnum Tipo { get; set; }
    }
}
