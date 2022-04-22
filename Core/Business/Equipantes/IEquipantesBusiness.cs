using Core.Models.Equipantes;
using System.Linq;

namespace Core.Business.Equipantes
{
    public interface IEquipantesBusiness
    {
<<<<<<< HEAD
        IQueryable<Data.Entities.Equipante> GetEquipantes();
        Data.Entities.Equipante GetEquipanteById(int id);
        Data.Entities.Equipante PostEquipante(PostEquipanteModel model);
=======
        IQueryable<Data.Entities.Equipante> GetEquipantes();        
        Data.Entities.Equipante GetEquipanteById(int id);
        void PostEquipante(PostEquipanteModel model);
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
        void DeleteEquipante(int id);
        void ToggleSexo(int id);
        void ToggleVacina(int id);
        void ToggleTeste(int id);
<<<<<<< HEAD
        void ToggleCheckin(int id, int eventoid);
=======
        void ToggleCheckin(int id);
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
    }
}
