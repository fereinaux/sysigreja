using Data.Entities.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Utils.Enums;

namespace Data.Entities
{
    public class Evento : StatusBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Descricao { get; set; }
        public int Numeracao { get; set; }
        public int? Capacidade { get; set; }
        public DateTime DataEvento { get; set; }
        public virtual TiposEventoEnum TipoEvento { get; set; }
        public int Valor { get; set; }
<<<<<<< HEAD
        public int ValorTaxa { get; set; }

=======
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
    }
}