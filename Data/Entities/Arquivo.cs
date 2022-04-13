using Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Data.Entities
{
    public class Arquivo : UsuarioBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Tipo { get; set; }
        public string Extensao { get; set; }
        public byte[] Conteudo { get; set; }
        public bool IsFoto{ get; set; }
        public int? EventoId { get; set; }
        public Evento  Evento { get; set; }
        public int? EquipanteId { get; set; }
        public Equipante Equipante { get; set; }
        public int? ParticipanteId { get; set; }
        public Participante Participante { get; set; }
        public int? LancamentoId { get; set; }
        public Lancamento Lancamento { get; set; }
    }
}