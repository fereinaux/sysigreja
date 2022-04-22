using Data.Context;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using Utils.Enums;
<<<<<<< HEAD
=======
using Utils.Extensions;
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566

namespace Data.Entities.Base
{
    public class StatusBase : UsuarioBase
    {
        public virtual StatusEnum Status { get; set; }
        [ForeignKey("UsuarioDelecao")]
        public string UsuarioDelecaoId { get; set; }
        public virtual ApplicationUser UsuarioDelecao { get; set; }
        public DateTime? DataDelecao { get; set; }
    }
}