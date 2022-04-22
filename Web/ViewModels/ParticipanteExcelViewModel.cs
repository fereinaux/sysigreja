using System.ComponentModel;

namespace SysIgreja.ViewModels
{
    public class ParticipanteExcelViewModel
    {
        public string Nome { get; set; }
        public string Apelido { get; set; }
        [DisplayName("Data de Nascimento")]
        public string DataNascimento { get; set; }
        public int Idade { get; set; }
        public string Sexo { get; set; }
        public string Email { get; set; }
        public string Fone { get; set; }
<<<<<<< HEAD
        [DisplayName("Nome da Mãe")]
        public string NomeMae { get; set; }
        [DisplayName("Fone da Mãe")]
        public string FoneMae { get; set; }
        [DisplayName("Nome do Pai")]
        public string NomePai { get; set; }
        [DisplayName("Fone do Pai")]
        public string FonePai { get; set; }
=======
        [DisplayName("Endereço")]
        public string Endereco { get; set; }
        public string Bairro { get; set; }
        [DisplayName("Congregação")]
        public string Congregacao { get; set; }
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566

        [DisplayName("Nome do Contato")]
        public string NomeConvite { get; set; }
        [DisplayName("Fone do Contato")]
        public string FoneConvite { get; set; }
        [DisplayName("Restrição Alimentar")]
        public string RestricaoAlimentar { get; set; }
        [DisplayName("Medicação")]
        public string Medicacao { get; set; }
        [DisplayName("Alergia")]

        public string Alergia { get; set; }
        [DisplayName("Nome do Parente")]
        public string NomeParente { get; set; }
        [DisplayName("Situação")]
        public string Situacao { get; set; }
    }
}
