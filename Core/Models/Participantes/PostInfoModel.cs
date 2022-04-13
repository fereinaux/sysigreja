using System;
using Utils.Enums;

namespace Core.Models.Participantes
{
    public class PostInfoModel
    {
        public int Id { get; set; }    
        public bool MsgPagamento { get; set; }
        public bool MsgVacina { get; set; }
        public bool MsgGeral { get; set; }
        public bool MsgFoto { get; set; }
        public bool MsgNoitita { get; set; }
        public string Observacao { get; set; }
        public string[] Etiquetas { get; set; }
    }
}
