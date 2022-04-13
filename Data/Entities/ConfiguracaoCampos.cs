using Data.Entities.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Utils.Enums;

namespace Data.Entities
{
    public class ConfiguracaoCampos : UsuarioBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public CamposEnum Campo { get; set; }
    }
}
