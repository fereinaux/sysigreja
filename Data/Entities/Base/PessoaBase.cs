using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using Utils.Enums;
using Utils.Extensions;
using Utils.Services;

namespace Data.Entities.Base
{
    public class PessoaBase : StatusBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        private string nome;
        public string Nome
        {
            get { return UtilServices.CapitalizarNome(nome); }
            set { nome = value; }
        }
        private string apelido;
        public string Apelido
        {
            get { return UtilServices.CapitalizarNome(apelido); }
            set { apelido = value; }
        }
        public DateTime? DataNascimento { get; set; }
     
        public string Email { get; set; }
        public string Fone { get; set; }
        public string Profissão { get; set; }
        public string Instagram { get; set; }
        public bool HasRestricaoAlimentar { get; set; }
        public string RestricaoAlimentar { get; set; }
        public bool HasVacina { get; set; }
        public bool HasTeste { get; set; }
        public bool Checkin { get; set; }
        public bool HasMedicacao { get; set; }
        public string Medicacao { get; set; }
        public bool HasAlergia { get; set; }
        public string Alergia { get; set; }
        public string CEP { get; set; }
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Referencia { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }

        public SexoEnum Sexo { get; set; }

        public static int GetAge(DateTime customerDateOfBirth)
        {
            var now = DateTime.UtcNow;
            var customerAge = now.Year - customerDateOfBirth.Year;
            if (customerDateOfBirth > now.AddYears(-customerAge))
                customerAge--;
            return customerAge;
        }
    }
}