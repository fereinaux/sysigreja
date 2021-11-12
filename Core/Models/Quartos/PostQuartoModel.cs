using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.Enums;

namespace Core.Models.Quartos
{
    public class PostQuartoModel
    {
        public int Id { get; set; }
        public int EventoId { get; set; }
        public string Titulo { get; set; }
        public SexoEnum Sexo { get; set; }
        public int Capacidade { get; set; }
    }
}
