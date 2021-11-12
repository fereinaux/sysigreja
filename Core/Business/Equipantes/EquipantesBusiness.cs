﻿using Core.Models.Equipantes;
using Core.Models.Eventos;
using Data.Entities;
using Data.Repository;
using System.Linq;
using System.Data.Entity;
using Utils.Enums;
using Utils.Extensions;

namespace Core.Business.Equipantes
{
    public class EquipantesBusiness : IEquipantesBusiness
    {
        private readonly IGenericRepository<Equipante> equipanteRepository;        

        public EquipantesBusiness(IGenericRepository<Equipante> equipanteRepository)
        {
            this.equipanteRepository = equipanteRepository;            
        }

        public void DeleteEquipante(int id)
        {
            equipanteRepository.Delete(id);
            equipanteRepository.Save();
        }

        public Equipante GetEquipanteById(int id)
        {
            return equipanteRepository.GetById(id);
        }

        public IQueryable<Equipante> GetEquipantes()
        {            
            return equipanteRepository.GetAll();
        }

        public void PostEquipante(PostEquipanteModel model)
        {
            Equipante equipante = null;

            if (model.Id > 0)
            {
                equipante = equipanteRepository.GetById(model.Id);

                equipante.Nome = model.Nome;
                equipante.Apelido = model.Apelido;
                equipante.DataNascimento = model.DataNascimento.AddHours(5);
                equipante.Fone = model.Fone;
                equipante.Email = model.Email;
                equipante.HasAlergia = model.HasAlergia;
                equipante.Alergia = model.HasAlergia ? model.Alergia : null;
                equipante.HasMedicacao = model.HasMedicacao;
                equipante.Medicacao = model.HasMedicacao ? model.Medicacao : null;
                equipante.HasRestricaoAlimentar = model.HasRestricaoAlimentar;
                equipante.RestricaoAlimentar = model.HasRestricaoAlimentar ? model.RestricaoAlimentar : null;
                equipante.Sexo = model.Sexo;

                equipanteRepository.Update(equipante);
            }
            else
            {                       
                equipante = new Equipante
                {
                    Nome = model.Nome,
                    Apelido = model.Apelido,
                    DataNascimento = model.DataNascimento.AddHours(5),
                    Fone = model.Fone,
                    Email = model.Email,
                    Status = StatusEnum.Ativo,
                    HasAlergia = model.HasAlergia,
                    Alergia = model.HasAlergia ? model.Alergia : null,
                    HasMedicacao = model.HasMedicacao,
                    Medicacao = model.HasMedicacao ? model.Medicacao : null,
                    HasRestricaoAlimentar = model.HasRestricaoAlimentar,
                    RestricaoAlimentar = model.HasRestricaoAlimentar ? model.RestricaoAlimentar : null,
                    Sexo = model.Sexo
                };

                equipanteRepository.Insert(equipante);
            }         
            
            equipanteRepository.Save();
        }

        public void ToggleSexo(int id)
        {
            var equipante = GetEquipanteById(id);
            equipante.Sexo = equipante.Sexo == SexoEnum.Feminino ? SexoEnum.Masculino : SexoEnum.Feminino;
            equipanteRepository.Update(equipante);
            equipanteRepository.Save();
        }

        public void ToggleVacina(int id)
        {
            var equipante = GetEquipanteById(id);
            equipante.HasVacina = !equipante.HasVacina;
            equipanteRepository.Update(equipante);
            equipanteRepository.Save();
        }

        public void ToggleTeste(int id)
        {
            var equipante = GetEquipanteById(id);
            equipante.HasTeste = !equipante.HasTeste;
            equipanteRepository.Update(equipante);
            equipanteRepository.Save();
        }

        public void ToggleCheckin(int id)
        {
            var equipante = GetEquipanteById(id);
            equipante.Checkin = !equipante.Checkin;
            equipanteRepository.Update(equipante);
            equipanteRepository.Save();
        }
    }
}
