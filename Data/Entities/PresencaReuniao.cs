<<<<<<< HEAD
﻿using Data.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
    public class PresencaReuniao : UsuarioBase
<<<<<<< HEAD
    {
=======
    {   
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int ReuniaoId { get; set; }
        public ReuniaoEvento Reuniao { get; set; }
        public int EquipanteEventoId { get; set; }
        public EquipanteEvento EquipanteEvento { get; set; }
    }
}