using Core.Models.DataTable;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Utils.Enums;

namespace Core.Models.Equipantes
{

    public class FilterModel
    {
        public int EventoId { get; set; }
        public EquipesEnum? Equipe { get; set; }
        public int Start { get; set; }
        public int Length { get; set; }
        public List<Column> columns { get; set; }
        public Search search { get; set; }
        public List<Order> order { get; set; }

    }
    public class EquipanteListModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Apelido { get; set; }
        public string Equipe { get; set; }
        public string Status { get; set; }
        public int Idade { get; set; }
        public int Faltas { get; set; }
        public int QtdAnexos { get; set; }
        public string Sexo { get; set; }
        public string Fone { get; set; }
        public bool HasVacina { get; set; }
        public bool HasFoto { get; set; }
        public bool HasOferta { get; set; }
        public string DataNascimento { get; set; }
        public string Email { get; set; }
        public string Alergia { get; set; }
        public string Medicacao { get; set; }
        public string RestricaoAlimentar { get; set; }
    }


    public class EquipanteExcelModel
    {
        public string Sexo { get; set; }
        public string Nome { get; set; }
        public string Apelido { get; set; }
        public string Status { get; set; }
        [DisplayName("Data de Nascimento")]
        public string DataNascimento { get; set; }
        public int Idade { get; set; }
        public string Fone { get; set; }
        public string Email { get; set; }
        public string Equipe { get; set; }
        [DisplayName("Oferta de Amor")]
        public string HasOferta { get; set; }
        [DisplayName("Comprovante da Vacina")]
        public string HasVacina { get; set; }
        public string Alergia { get; set; }
        [DisplayName("Medicação")]
        public string Medicacao { get; set; }
        [DisplayName("Restrição Alimentar")]
        public string RestricaoAlimentar { get; set; }
    }
}
