<<<<<<< HEAD
﻿using Data.Entities.Base;
=======
﻿using Data.Context;
using Data.Entities.Base;
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
<<<<<<< HEAD
=======
using Utils.Enums;
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566

namespace Data.Entities
{
    public class ReuniaoEvento : StatusBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int EventoId { get; set; }
        public Evento Evento { get; set; }
        public DateTime DataReuniao { get; set; }
        public ICollection<PresencaReuniao> Presenca { get; set; }
    }
}