<<<<<<< HEAD
﻿using Data.Entities.Base;
=======
﻿using Data.Context;
using Data.Entities.Base;
using System;
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Utils.Enums;

namespace Data.Entities
{
    public class EquipanteEvento : UsuarioBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int EventoId { get; set; }
        public Evento Evento { get; set; }
        public int EquipanteId { get; set; }
        public Equipante Equipante { get; set; }
<<<<<<< HEAD
        public bool Checkin { get; set; }
=======
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
        public EquipesEnum Equipe { get; set; }
        public TiposEquipeEnum Tipo { get; set; }
        public virtual ICollection<PresencaReuniao> Presencas { get; set; }
    }
}