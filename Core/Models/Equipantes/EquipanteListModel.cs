using Core.Models.DataTable;
using System;
using System.Collections.Generic;
using Utils.Enums;

namespace Core.Models.Equipantes
{

    public class FilterModel
    {
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
        public int Idade { get; set; }
        public int Faltas { get; set; }
        public int QtdAnexos { get; set; }
        public string Sexo { get; set; }
        public string Fone { get; set; }
        public bool HasVacina { get; set; }
        public bool HasFoto { get; set; }
     
    }
}
