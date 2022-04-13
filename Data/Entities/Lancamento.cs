using Data.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Utils.Enums;

namespace Data.Entities
{
    public class Lancamento : StatusBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public decimal Valor { get; set; }
        public string Descricao { get; set; }
        public string Observacao { get; set; }
        public TiposLancamentoEnum Tipo { get; set; }
        public int MeioPagamentoId { get; set; }
        public MeioPagamento MeioPagamento { get; set; }
        public int EventoId { get; set; }
        public Evento Evento { get; set; }
        public int? EquipanteId { get; set; }
        public Equipante Equipante { get; set; }
        public int? ParticipanteId { get; set; }
        public Participante Participante { get; set; }
        public int? CentroCustoId { get; set; }
        public CentroCusto CentroCusto { get; set; }
        public int? ContaBancariaId { get; set; }
        public ContaBancaria ContaBancaria { get; set; }
    }
}
