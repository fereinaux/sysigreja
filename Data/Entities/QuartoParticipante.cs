using Data.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace Data.Entities
{
    public class QuartoParticipante : UsuarioBase
    {
        [Key]
        public int Id { get; set; }
        public int QuartoId { get; set; }
        public Quarto Quarto { get; set; }
        public int? ParticipanteId { get; set; }
        public Participante Participante { get; set; }
        public int? EquipanteId { get; set; }
        public Equipante Equipante { get; set; }
    }
}