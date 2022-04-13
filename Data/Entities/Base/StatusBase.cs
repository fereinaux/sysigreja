using Data.Context;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using Utils.Enums;
using Utils.Extensions;

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