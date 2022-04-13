using Data.Entities.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    public class Equipante : PessoaBase
    {
        [NotMapped]
        public string Equipe { get; set; }
        public virtual ICollection<EquipanteEvento> Equipes { get; set; }
        public virtual ICollection<Arquivo> Arquivos { get; set; }
        public virtual ICollection<Lancamento> Lancamentos { get; set; }

    }
}