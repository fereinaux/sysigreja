using Core.Business.Eventos;
using Core.Models.Equipantes;
using Data.Entities;
using Data.Repository;
using System;
using System.Data.Entity;
using System.Linq;
using Utils.Enums;

namespace Core.Business.Equipantes
{
    public class EquipantesBusiness : IEquipantesBusiness
    {
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
        }

        public void DeleteEquipante(int id)
        {
            equipanteRepository.Delete(id);
            equipanteRepository.Save();
        }

        public Equipante GetEquipanteById(int id)
        {

            return equipanteRepository.GetAll(x => x.Id == id).Include(x => x.ParticipantesEtiquetas).Include(x => x.ParticipantesEtiquetas.Select(y => y.Etiqueta)).SingleOrDefault();
        }

        public IQueryable<Equipante> GetEquipantes()
        {
            return equipanteRepository.GetAll().Include(x => x.ParticipantesEtiquetas).Include(x => x.ParticipantesEtiquetas.Select(y => y.Etiqueta));
        }

        public Equipante PostEquipante(PostEquipanteModel model)
        {
            Equipante equipante = null;

            if (model.Id > 0)
            {
                equipante = equipanteRepository.GetById(model.Id);

                equipante.Nome = model.Nome;
                equipante.Apelido = model.Apelido;
                equipante.DataNascimento = model.DataNascimento?.AddHours(5);
                equipante.Fone = model.Fone;
                equipante.Email = model.Email;
                equipante.HasAlergia = model.HasAlergia;
                equipante.Alergia = model.HasAlergia ? model.Alergia : null;
                equipante.HasMedicacao = model.HasMedicacao;
                equipante.Medicacao = model.HasMedicacao ? model.Medicacao : null;
                equipante.HasRestricaoAlimentar = model.HasRestricaoAlimentar;
                equipante.RestricaoAlimentar = model.HasRestricaoAlimentar ? model.RestricaoAlimentar : null;
                equipante.Sexo = model.Sexo;
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

                equipanteRepository.Update(equipante);
            }
            else
            {
                equipante = new Equipante
                {
                    Nome = model.Nome,
                    Apelido = model.Apelido,
                    DataNascimento = model.DataNascimento?.AddHours(5),
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
            return equipante;
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

        public void ToggleCheckin(int id, int eventoid)
        {
            var equipante = equipanteEventoRepository.GetAll(x => x.EventoId == eventoid && x.EquipanteId == id).FirstOrDefault();
            equipante.Checkin = !equipante.Checkin;
            equipanteEventoRepository.Update(equipante);
            equipanteEventoRepository.Save();
        }
    }
}
