using System;

namespace Arquitetura.ViewModels
{
    public class EventoViewModel
    {
        public int Id { get; set; }
        public int Numeracao { get; set; }
        public int? Capacidade { get; set; }
        public DateTime DataEvento { get; set; }
        public string TipoEvento { get; set; }
        public string Status { get; set; }
        public string Valor { get; set; }
        public int QtdAnexos { get; set; }
    }

    
}
