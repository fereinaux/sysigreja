
using Data.Context;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities.Base
{
    public class UsuarioBase
    {
        [ForeignKey("UsuarioCadastro")]
        public string UsuarioCadastroId { get; set; }
        public virtual ApplicationUser UsuarioCadastro { get; set; }
        public DateTime? DataCadastro { get; set; }

        [ForeignKey("UsuarioEdicao")]
        public string UsuarioEdicaoId { get; set; }
        public virtual ApplicationUser UsuarioEdicao { get; set; }
        public DateTime? DataEdicao { get; set; }
    }
}