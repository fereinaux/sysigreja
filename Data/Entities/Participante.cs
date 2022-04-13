using Data.Context;
using Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

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
        public virtual ICollection<ParticipantesEtiquetas> ParticipantesEtiquetas { get; set; }

        public string NomePai { get; set; }
        public string FonePai { get; set; }

        public string NomeMae { get; set; }
        public string FoneMae { get; set; }
        public string ReferenciaPagSeguro { get; set; }

        public string NomeConvite { get; set; }
        public string FoneConvite { get; set; }

        public string NomeContato { get; set; }
        public string FoneContato { get; set; }
        public string Congregacao { get; set; }
        public bool? HasParente { get; set; }
        public string Parente { get; set; }
        public bool PendenciaContato { get; set; }
        public bool Carona { get; set; }
        public bool Boleto { get; set; }
        public bool PendenciaBoleto { get; set; }    
        public bool MsgPagamento { get; set; }
        public bool MsgVacina { get; set; }
        public bool MsgGeral { get; set; }
        public bool MsgNoitita { get; set; }
        public bool MsgFoto { get; set; }
        public string Observacao { get; set; }
    }
}