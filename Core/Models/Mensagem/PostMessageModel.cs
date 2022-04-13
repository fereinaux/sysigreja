using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.Enums;

namespace Core.Models.Mensagem
{
    public class PostMessageModel
    {
        public int Id { get; set; }
        public string Conteudo { get; set; }
        public string Titulo { get; set; }
    }
}
