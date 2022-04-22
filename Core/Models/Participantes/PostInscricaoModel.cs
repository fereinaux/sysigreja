<<<<<<< HEAD
﻿using Core.Models.Etiquetas;
using System;
using System.Collections.Generic;
=======
﻿using System;
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
using Utils.Enums;

namespace Core.Models.Participantes
{
    public class PostInscricaoModel
    {
        public bool HasTeste;

        public int Id { get; set; }
        public int EventoId { get; set; }
        public string Nome { get; set; }
        public string Apelido { get; set; }
<<<<<<< HEAD
        public DateTime? DataNascimento { get; set; }
=======
        public DateTime DataNascimento { get; set; }
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
        public string Email { get; set; }
        public string Fone { get; set; }
        public string Padrinho { get; set; }
        public bool Checkin { get; set; }
        public bool CancelarCheckin { get; set; }
        public bool HasRestricaoAlimentar { get; set; }
        public string RestricaoAlimentar { get; set; }
        public bool HasMedicacao { get; set; }
        public string Medicacao { get; set; }
        public bool HasAlergia { get; set; }
        public bool HasFoto { get; set; }
        public string Alergia { get; set; }
        public bool HasParente { get; set; }
        public string Parente { get; set; }
<<<<<<< HEAD
        public string CEP { get; set; }
        public string Logradouro { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
=======
        public string Logradouro { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
        public SexoEnum Sexo { get; set; }
        public string Congregacao { get; set; }
        public string NomePai { get; set; }
        public string FonePai { get; set; }
        public string NomeMae { get; set; }
        public string FoneMae { get; set; }
        public string Foto { get; set; }
        public string NomeConvite { get; set; }
        public string FoneConvite { get; set; }
<<<<<<< HEAD
        public string NomeContato { get; set; }
        public string FoneContato { get; set; }
=======
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
        public bool PendenciaContato { get; set; }
        public bool Boleto { get; set; }
        public bool PendenciaBoleto { get; set; }
        public string Status { get; set; }
        public bool MsgPagamento { get; set; }
        public bool HasVacina { get; set; }
        public bool MsgVacina { get; set; }
        public bool MsgGeral { get; set; }
        public bool MsgNoitita { get; set; }
        public bool MsgFoto { get; set; }
        public string Observacao { get; set; }
<<<<<<< HEAD
        public IEnumerable<PostEtiquetaModel> Etiquetas { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public string Referencia { get; set; }
        public string Numero { get; set; }
        public string Estado { get; set; }
=======
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
    }
}
