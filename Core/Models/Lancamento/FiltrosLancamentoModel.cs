using System;
using Utils.Enums;

namespace Core.Models.Lancamento
{
    public class FiltrosLancamentoModel
    {
        public DateTime? DataIni { get; set; }
        public DateTime? DataFim { get; set; }
        public int? EventoId { get; set; }
        public int? CentroCustoId { get; set; }
        public int? MeioPagamentoId { get; set; }
        public int? ContaBancariaId { get; set; }
    }
}
