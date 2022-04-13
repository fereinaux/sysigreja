using System;
using Utils.Enums;

namespace Core.Models.Lancamento
{
    public class PostPagamentoModel
    {
        public int? ParticipanteId { get; set; }
        public int? EquipanteId { get; set; }
        public int EventoId { get; set; }
        public int MeioPagamentoId { get; set; }
        public int ContaBancariaId { get; set; }
        public decimal Valor { get; set; }
    }
}
