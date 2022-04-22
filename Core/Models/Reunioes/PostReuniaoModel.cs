using System;

namespace Core.Models.Reunioes
{
    public class PostReuniaoModel
    {
        public int Id { get; set; }
        public int EventoId { get; set; }
        public DateTime DataReuniao { get; set; }
    }
}
