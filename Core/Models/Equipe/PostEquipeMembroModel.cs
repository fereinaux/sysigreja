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
