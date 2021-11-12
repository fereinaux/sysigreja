using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.Enums;

namespace Core.Models.Equipe
{
    public class PostEquipeMembroModel
    {
        public int EquipanteId { get; set; }        
        public int EventoId { get; set; }        
        public EquipesEnum Equipe { get; set; }        
    }
}
