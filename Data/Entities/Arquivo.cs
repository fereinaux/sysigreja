using Data.Entities.Base;
<<<<<<< HEAD
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
=======
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566

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
<<<<<<< HEAD
        public bool IsFoto { get; set; }
        public int? EventoId { get; set; }
        public Evento Evento { get; set; }
=======
        public bool IsFoto{ get; set; }
        public int? EventoId { get; set; }
        public Evento  Evento { get; set; }
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
        public int? EquipanteId { get; set; }
        public Equipante Equipante { get; set; }
        public int? ParticipanteId { get; set; }
        public Participante Participante { get; set; }
        public int? LancamentoId { get; set; }
        public Lancamento Lancamento { get; set; }
    }
}