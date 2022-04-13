using Data.Context;
using Data.Entities.Base;
using System;
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
        public int Capacidade { get; set; }
    }
}