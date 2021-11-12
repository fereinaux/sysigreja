using Data.Context;
using Data.Entities.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Utils.Enums;

namespace Data.Entities
{
    public class CirculoParticipante : UsuarioBase
    {
        [Key]
        public int Id { get; set; }
        public int CirculoId { get; set; }
        public Circulo Circulo { get; set; }
        public int ParticipanteId { get; set; }
        public Participante Participante { get; set; }
    }
}