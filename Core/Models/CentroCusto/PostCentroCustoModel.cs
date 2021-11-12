using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.Enums;

namespace Core.Models.CentroCusto
{
    public class PostCentroCustoModel
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public TiposCentroCustoEnum Tipo { get; set; }
    }
}
