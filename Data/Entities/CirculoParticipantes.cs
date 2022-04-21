using Data.Entities.Base;
using System.ComponentModel.DataAnnotations;

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