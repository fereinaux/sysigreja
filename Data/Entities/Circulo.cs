using Data.Context;
using Data.Entities.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Utils.Enums;

namespace Data.Entities
{
    public class Circulo : UsuarioBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int EventoId { get; set; }
        [ForeignKey("EventoId")]
        public Evento Evento { get; set; }
        public int? Dirigente1Id { get; set; }
        [ForeignKey("Dirigente1Id")]
        public EquipanteEvento Dirigente1 { get; set; }
        public int? Dirigente2Id { get; set; }
        [ForeignKey("Dirigente2Id")]
        public EquipanteEvento Dirigente2 { get; set; }
        public CoresEnum Cor { get; set; }
    }
}