﻿using Data.Context;
using Data.Entities;
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
        void DeleteUsuario(string id);
    }
}
