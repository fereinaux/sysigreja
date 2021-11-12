using Data.Context;
using Data.Entities.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Utils.Enums;

namespace Data.Entities
{
    public class PresencaReuniao : UsuarioBase
    {   
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int ReuniaoId { get; set; }
        public ReuniaoEvento Reuniao { get; set; }
        public int EquipanteEventoId { get; set; }
        public EquipanteEvento EquipanteEvento { get; set; }
    }
}