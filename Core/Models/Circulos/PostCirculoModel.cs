<<<<<<< HEAD
﻿using Utils.Enums;
=======
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.Enums;
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566

namespace Core.Models.Circulos
{
    public class PostCirculoModel
    {
        public int Id { get; set; }
        public int EventoId { get; set; }
        public int Dirigente1Id { get; set; }
        public int Dirigente2Id { get; set; }
        public CoresEnum Cor { get; set; }
    }
}
