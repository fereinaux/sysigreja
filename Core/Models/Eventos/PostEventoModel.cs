using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Eventos
{
    public class PostEventoModel
    {
        public int Id { get; set; }
        public int Numeracao { get; set; }
        public int Capacidade { get; set; }
        public int TipoEvento { get; set; }
        public int Valor { get; set; }
        public DateTime DataEvento { get; set; }
    }
}
