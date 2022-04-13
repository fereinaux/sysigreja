using System;
using Utils.Enums;

namespace Core.Models.Lancamento
{
    public class GetPagamentosModel
    {
        public int? ParticipanteId { get; set; }
        public int? EquipanteId { get; set; }
    }
}
