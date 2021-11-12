using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Core.Models.Arquivos
{
    public class PostArquivoModel
    {
        public int? EventoId { get; set; }
        public int? ParticipanteId { get; set; }
        public int? EquipanteId { get; set; }
        public int? LancamentoId { get; set; }
        public bool IsFoto{ get; set; }
        public HttpPostedFileBase Arquivo { get; set; }
    }
}
