using Data.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    public class MeioPagamento : StatusBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Descricao { get; set; }
        public double Taxa { get; set; }
        public bool IsEditavel { get; set; }
    }
}
