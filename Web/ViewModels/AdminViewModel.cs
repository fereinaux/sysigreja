using Data.Entities;
using System.Collections.Generic;

namespace SysIgreja.ViewModels
{
    public class AdminViewModel
    {
        public string TotalReceber { get; set; }
        public string TotalPagar { get; set; }
        public string Evento { get; set; }
        public int Total { get; set; }
        public int Boletos { get; set; }
        public int Contatos { get; set; }
        public int Meninos { get; set; }
        public int Meninas { get; set; }
        public int Confirmados { get; set; }
        public int Cancelados { get; set; }
        public int Presentes { get; set; }
        public int Isencoes { get; set; }
        public List<ParticipanteViewModel> UltimosInscritos { get; set; }
        public List<ListaEquipesViewModel> Equipes { get; set; }
        public List<ReuniaoViewModel> Reunioes { get; set; }
    }

    
}
