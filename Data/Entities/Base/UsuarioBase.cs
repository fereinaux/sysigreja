
using Data.Context;
using System;
<<<<<<< HEAD
=======
using System.ComponentModel.DataAnnotations;
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
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