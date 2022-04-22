<<<<<<< HEAD
﻿using Utils.Enums;
=======
﻿using System;
using Utils.Enums;
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566

namespace Core.Models.Lancamento
{
    public class PostLancamentoModel
    {
        public int Id { get; set; }
        public TiposLancamentoEnum Tipo { get; set; }
        public string Descricao { get; set; }
        public string Observacao { get; set; }
        public int MeioPagamentoId { get; set; }
        public int CentCustoId { get; set; }
        public int EventoId { get; set; }
        public int ContaBancariaId { get; set; }
        public decimal Valor { get; set; }
    }
}
