using Data.Entities.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    public class Equipante : PessoaBase
    {
        [NotMapped]
        public string Equipe { get; set; }
<<<<<<< HEAD
        public virtual ICollection<ParticipantesEtiquetas> ParticipantesEtiquetas { get; set; }
        public virtual ICollection<EquipanteEvento> Equipes { get; set; }
        public virtual ICollection<Arquivo> Arquivos { get; set; }
        public virtual ICollection<Lancamento> Lancamentos { get; set; }

=======
        public virtual ICollection<EquipanteEvento> Equipes { get; set; }
        public virtual ICollection<Arquivo> Arquivos { get; set; }
      
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
    }
}