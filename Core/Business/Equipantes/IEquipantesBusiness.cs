using Core.Models.Equipantes;
using System.Linq;

namespace Core.Business.Equipantes
{
    public interface IEquipantesBusiness
    {
        IQueryable<Data.Entities.Equipante> GetEquipantes();        
        Data.Entities.Equipante GetEquipanteById(int id);
        void PostEquipante(PostEquipanteModel model);
        void DeleteEquipante(int id);
        void ToggleSexo(int id);
        void ToggleVacina(int id);
        void ToggleTeste(int id);
        void ToggleCheckin(int id);
    }
}
