using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.Enums;

namespace Core.Models.ContaBancaria
{
    public class PostContaBancariaModel
    {
        public int Id { get; set; }
        public string Conta { get; set; }
        public string Agencia { get; set; }
        public string Operacao { get; set; }
        public string CPF { get; set; }
        public string Nome { get; set; }
        public BancosEnum Banco { get; set; }
    }
}
