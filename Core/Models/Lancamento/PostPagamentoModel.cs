<<<<<<< HEAD
﻿namespace Core.Models.Lancamento
=======
﻿using System;
using Utils.Enums;

namespace Core.Models.Lancamento
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
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
