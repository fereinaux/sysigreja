<<<<<<< HEAD
﻿namespace Core.Models.Participantes
{
    public class PostInfoModel
    {
        public int Id { get; set; }
=======
﻿using System;
using Utils.Enums;

namespace Core.Models.Participantes
{
    public class PostInfoModel
    {
        public int Id { get; set; }    
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
        public bool MsgPagamento { get; set; }
        public bool MsgVacina { get; set; }
        public bool MsgGeral { get; set; }
        public bool MsgFoto { get; set; }
        public bool MsgNoitita { get; set; }
        public string Observacao { get; set; }
<<<<<<< HEAD
        public string[] Etiquetas { get; set; }
=======
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
    }
}
