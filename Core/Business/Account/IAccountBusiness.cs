<<<<<<< HEAD
﻿using Data.Context;
using Data.Entities;
=======
﻿using Core.Models.Eventos;
using Data.Context;
using Data.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
using System.Collections.Generic;
using System.Linq;

namespace Core.Business.Account
{
    public interface IAccountBusiness
    {
        void Seed();
        IQueryable<ApplicationUser> GetUsuarios();
        List<Equipante> GetEquipantesUsuario(string idUsuario);
        ApplicationUser GetUsuarioById(string id);
        void ToggleUsuarioStatus(string id);
<<<<<<< HEAD
        void DeleteUsuario(string id);
=======
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
    }
}
