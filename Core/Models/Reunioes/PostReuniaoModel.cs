using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Reunioes
{
    public class PostReuniaoModel
    {
        public int Id { get; set; }
        public int EventoId { get; set; }
        public DateTime DataReuniao { get; set; }
    }
}
