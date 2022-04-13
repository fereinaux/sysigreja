using Core.Models.DataTable;
using Core.Models.Etiquetas;
using System;
using System.Collections.Generic;
using Utils.Enums;

namespace Core.Models.Participantes
{
   
    public class FilterModel
    {
        public int EventoId { get; set; }
        public int Start { get; set; }
        public int Length { get; set; }
        public List<Column> columns { get; set; }
        public Search search { get; set; }
        public List<Order> order { get; set; }
        public int PadrinhoId { get; set; }
        public StatusEnum? Status { get; set; }
        public List<string> Etiquetas { get; set; }
        public List<string> NaoEtiquetas { get; set; }

    }
    public class ParticipanteListModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Apelido { get; set; }
        public int Idade { get; set; }
        public int QtdAnexos { get; set; }
        public string Sexo { get; set; }
        public string Fone { get; set; }
        public string Status { get; set; }
        public string Circulo { get; set; }
        public bool Checkin { get; set; }
        public bool HasVacina { get; set; }
        public bool HasFoto { get; set; }
        public bool HasContact { get; set; }
        public string Padrinho { get; set; }
        public string NomePai { get; set; }
        public string FonePai { get; set; }
        public string NomeMae { get; set; }
        public string FoneMae { get; set; }
        public string NomeConvite { get; set; }
        public string FoneConvite { get; set; }
        public string CEP { get; set; }
        public string Logradouro { get; set; }
        public string Bairro { get; set; }

        public IEnumerable<PostEtiquetaModel> Etiquetas { get; set; }

    }

    public class ParticipanteSelectModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
      

    }
}
