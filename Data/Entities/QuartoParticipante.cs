using Data.Context;
using Data.Entities.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Utils.Enums;

namespace Data.Entities
{
    public class QuartoParticipante : UsuarioBase
    {
        [Key]
        public int Id { get; set; }
        public int QuartoId { get; set; }
        public Quarto Quarto { get; set; }
        public int ParticipanteId { get; set; }
        public Participante Participante { get; set; }
    }
}