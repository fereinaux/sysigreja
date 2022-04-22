<<<<<<< HEAD
﻿using Core.Business.Eventos;
using Core.Models.Equipantes;
using Data.Entities;
using Data.Repository;
using System;
using System.Data.Entity;
using System.Linq;
using Utils.Enums;
=======
﻿using Core.Models.Equipantes;
using Core.Models.Eventos;
using Data.Entities;
using Data.Repository;
using System.Linq;
using System.Data.Entity;
using Utils.Enums;
using Utils.Extensions;
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566

namespace Core.Business.Equipantes
{
    public class EquipantesBusiness : IEquipantesBusiness
    {
<<<<<<< HEAD
        private readonly IEventosBusiness eventosBusiness;
        private readonly IGenericRepository<Equipante> equipanteRepository;
        private readonly IGenericRepository<EquipanteEvento> equipanteEventoRepository;
        private readonly IGenericRepository<ParticipantesEtiquetas> ParticipantesEtiquetasRepo;

        public EquipantesBusiness(IGenericRepository<Equipante> equipanteRepository, IEventosBusiness eventosBusiness, IGenericRepository<ParticipantesEtiquetas> ParticipantesEtiquetasRepo, IGenericRepository<EquipanteEvento> equipanteEventoRepository)
        {
            this.equipanteRepository = equipanteRepository;
            this.eventosBusiness = eventosBusiness;
            this.equipanteEventoRepository = equipanteEventoRepository;
            this.ParticipantesEtiquetasRepo = ParticipantesEtiquetasRepo;
=======
        private readonly IGenericRepository<Equipante> equipanteRepository;        

        public EquipantesBusiness(IGenericRepository<Equipante> equipanteRepository)
        {
            this.equipanteRepository = equipanteRepository;            
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
        }

        public void DeleteEquipante(int id)
        {
            equipanteRepository.Delete(id);
            equipanteRepository.Save();
        }

        public Equipante GetEquipanteById(int id)
        {
<<<<<<< HEAD

            return equipanteRepository.GetAll(x => x.Id == id).Include(x => x.ParticipantesEtiquetas).Include(x => x.ParticipantesEtiquetas.Select(y => y.Etiqueta)).SingleOrDefault();
        }

        public IQueryable<Equipante> GetEquipantes()
        {
            return equipanteRepository.GetAll().Include(x => x.ParticipantesEtiquetas).Include(x => x.ParticipantesEtiquetas.Select(y => y.Etiqueta));
        }

        public Equipante PostEquipante(PostEquipanteModel model)
=======
            return equipanteRepository.GetById(id);
        }

        public IQueryable<Equipante> GetEquipantes()
        {            
            return equipanteRepository.GetAll();
        }

        public void PostEquipante(PostEquipanteModel model)
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
        {
            Equipante equipante = null;

            if (model.Id > 0)
            {
                equipante = equipanteRepository.GetById(model.Id);

                equipante.Nome = model.Nome;
                equipante.Apelido = model.Apelido;
<<<<<<< HEAD
                equipante.DataNascimento = model.DataNascimento?.AddHours(5);
=======
                equipante.DataNascimento = model.DataNascimento.AddHours(5);
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
                equipante.Fone = model.Fone;
                equipante.Email = model.Email;
                equipante.HasAlergia = model.HasAlergia;
                equipante.Alergia = model.HasAlergia ? model.Alergia : null;
                equipante.HasMedicacao = model.HasMedicacao;
                equipante.Medicacao = model.HasMedicacao ? model.Medicacao : null;
                equipante.HasRestricaoAlimentar = model.HasRestricaoAlimentar;
                equipante.RestricaoAlimentar = model.HasRestricaoAlimentar ? model.RestricaoAlimentar : null;
                equipante.Sexo = model.Sexo;
<<<<<<< HEAD
                equipante.HasVacina = model.HasVacina;
                var eventoAtivo = eventosBusiness.GetEventoAtivo();
                ParticipantesEtiquetasRepo.GetAll(x => x.EquipanteId == model.Id).ToList().ForEach(etiqueta => ParticipantesEtiquetasRepo.Delete(etiqueta.Id));
                if (model.Etiquetas != null)
                {
                    foreach (var etiqueta in model.Etiquetas)
                    {
                        ParticipantesEtiquetasRepo.Insert(new ParticipantesEtiquetas { EquipanteId = model.Id, EventoId = eventoAtivo?.Id ?? null, EtiquetaId = Int32.Parse(etiqueta) });
                    }

                }
                ParticipantesEtiquetasRepo.Save();
=======
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566

                equipanteRepository.Update(equipante);
            }
            else
<<<<<<< HEAD
            {
=======
            {                       
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
                equipante = new Equipante
                {
                    Nome = model.Nome,
                    Apelido = model.Apelido,
<<<<<<< HEAD
                    DataNascimento = model.DataNascimento?.AddHours(5),
=======
                    DataNascimento = model.DataNascimento.AddHours(5),
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
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
<<<<<<< HEAD
            }

            equipanteRepository.Save();
            return equipante;
=======
            }         
            
            equipanteRepository.Save();
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
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

<<<<<<< HEAD
        public void ToggleCheckin(int id, int eventoid)
        {
            var equipante = equipanteEventoRepository.GetAll(x => x.EventoId == eventoid && x.EquipanteId == id).FirstOrDefault();
            equipante.Checkin = !equipante.Checkin;
            equipanteEventoRepository.Update(equipante);
            equipanteEventoRepository.Save();
=======
        public void ToggleCheckin(int id)
        {
            var equipante = GetEquipanteById(id);
            equipante.Checkin = !equipante.Checkin;
            equipanteRepository.Update(equipante);
            equipanteRepository.Save();
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
        }
    }
}
