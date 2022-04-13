using System;
using System.Collections.Generic;

namespace SysIgreja.ViewModels
{
    public class ReuniaoViewModel
    {
        public int Id { get; set; }
        public DateTime DataReuniao { get; set; }
        public int Presenca { get; set; }
        public ICollection<EquipesModel> Equipes { get; set; }
    }

    public class EquipesModel
    {
        public string Equipe { get; set; }
        public string Presenca { get; set; }
        public decimal PresencaOrder { get; set; }
    }



}
