using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.Enums;

namespace Core.Models.Carona
{
    public class PostCaronaModel
    {
        public int Id { get; set; }
        public int EventoId { get; set; }
        public int MotoristaId { get; set; }
    }
}
