<<<<<<< HEAD
﻿using Data.Entities.Base;
using System.ComponentModel.DataAnnotations;
=======
﻿using Data.Context;
using Data.Entities.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Utils.Enums;
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566

namespace Data.Entities
{
    public class QuartoParticipante : UsuarioBase
    {
        [Key]
        public int Id { get; set; }
        public int QuartoId { get; set; }
        public Quarto Quarto { get; set; }
<<<<<<< HEAD
        public int? ParticipanteId { get; set; }
        public Participante Participante { get; set; }
        public int? EquipanteId { get; set; }
        public Equipante Equipante { get; set; }
=======
        public int ParticipanteId { get; set; }
        public Participante Participante { get; set; }
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
    }
}