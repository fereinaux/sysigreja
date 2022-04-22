<<<<<<< HEAD
﻿using Data.Entities.Base;
=======
﻿using Data.Context;
using Data.Entities.Base;
using System;
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Utils.Enums;

namespace Data.Entities
{
    public class Quarto : UsuarioBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int EventoId { get; set; }
        public Evento Evento { get; set; }
        public string Titulo { get; set; }
        public SexoEnum Sexo { get; set; }
<<<<<<< HEAD
        public TipoPessoaEnum TipoPessoa { get; set; }
=======
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
        public int Capacidade { get; set; }
    }
}