<<<<<<< HEAD
﻿using Data.Entities.Base;
using System.Collections.Generic;
=======
﻿using Data.Context;
using Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566

namespace Data.Entities
{
    public class
        Participante : PessoaBase
    {
        public int EventoId { get; set; }
        public Evento Evento { get; set; }

        public int? PadrinhoId { get; set; }
        public virtual Equipante Padrinho { get; set; }
        public virtual ICollection<Arquivo> Arquivos { get; set; }
        public virtual ICollection<CirculoParticipante> Circulos { get; set; }
<<<<<<< HEAD
        public virtual ICollection<ParticipantesEtiquetas> ParticipantesEtiquetas { get; set; }
=======
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566

        public string NomePai { get; set; }
        public string FonePai { get; set; }

        public string NomeMae { get; set; }
        public string FoneMae { get; set; }
        public string ReferenciaPagSeguro { get; set; }

        public string NomeConvite { get; set; }
        public string FoneConvite { get; set; }
<<<<<<< HEAD

        public string NomeContato { get; set; }
        public string FoneContato { get; set; }
=======
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
        public string Congregacao { get; set; }
        public bool? HasParente { get; set; }
        public string Parente { get; set; }
        public bool PendenciaContato { get; set; }
<<<<<<< HEAD
        public bool Carona { get; set; }
        public bool Boleto { get; set; }
        public bool PendenciaBoleto { get; set; }
=======
        public bool Boleto { get; set; }
        public bool PendenciaBoleto { get; set; }    
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
        public bool MsgPagamento { get; set; }
        public bool MsgVacina { get; set; }
        public bool MsgGeral { get; set; }
        public bool MsgNoitita { get; set; }
        public bool MsgFoto { get; set; }
        public string Observacao { get; set; }
    }
}