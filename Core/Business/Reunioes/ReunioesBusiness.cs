using Core.Business.Equipes;
<<<<<<< HEAD
=======
using Core.Models.Eventos;
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
using Core.Models.Reunioes;
using Data.Entities;
using Data.Repository;
using System.Data.Entity;
using System.Linq;
using Utils.Enums;

namespace Core.Business.Reunioes
{
    public class ReunioesBusiness : IReunioesBusiness
    {
        private readonly IGenericRepository<ReuniaoEvento> reuniaoRepository;
        private readonly IEquipesBusiness equipesBusiness;
        private readonly IGenericRepository<Equipante> equipanteRepository;

        public ReunioesBusiness(IGenericRepository<ReuniaoEvento> reuniaoRepository, IGenericRepository<Equipante> equipanteRepository, IEquipesBusiness equipesBusiness)
        {
            this.reuniaoRepository = reuniaoRepository;
            this.equipanteRepository = equipanteRepository;
            this.equipesBusiness = equipesBusiness;
<<<<<<< HEAD
        }
=======
    }
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566

        public void DeleteReuniao(int id)
        {
            reuniaoRepository.Delete(id);
            reuniaoRepository.Save();
        }

        public int? GetFaltasByEquipanteId(int equipanteId, int eventoId)
        {
<<<<<<< HEAD
            if (equipesBusiness.GetEquipeAtual(eventoId, equipanteId) != null)
                return reuniaoRepository.GetAll(x => x.EventoId == eventoId && x.DataReuniao < System.DateTime.Today && !x.Presenca.Any(y => y.EquipanteEvento.EquipanteId == equipanteId)).Include(x => x.Presenca).Count();
=======
            if (equipesBusiness.GetEquipeAtual(eventoId,equipanteId) != null)                          
                return reuniaoRepository.GetAll(x => x.EventoId == eventoId && x.DataReuniao < System.DateTime.Today && !x.Presenca.Any(y => y.EquipanteEvento.EquipanteId == equipanteId) ).Include(x => x.Presenca).Count();
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566

            return null;
        }

        public ReuniaoEvento GetReuniaoAtiva()
        {
            return reuniaoRepository.GetAll().OrderByDescending(x => x.DataReuniao).First();
        }

        public ReuniaoEvento GetReuniaoById(int id)
        {
            return reuniaoRepository.GetById(id);
        }

        public IQueryable<ReuniaoEvento> GetReunioes(int eventoId)
        {
<<<<<<< HEAD
            return reuniaoRepository.GetAll(x => x.EventoId == eventoId).Include(x => x.Presenca).Include(x => x.Presenca.Select(y => y.EquipanteEvento));
=======
            return reuniaoRepository.GetAll(x => x.EventoId == eventoId).Include(x => x.Presenca);
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
        }

        public void PostReuniao(PostReuniaoModel model)
        {
            ReuniaoEvento reuniao = null;

            if (model.Id > 0)
            {
                reuniao = reuniaoRepository.GetById(model.Id);
                reuniao.DataReuniao = model.DataReuniao.AddHours(4);

                reuniaoRepository.Update(reuniao);
            }
            else
            {
                reuniao = new ReuniaoEvento
                {
                    DataReuniao = model.DataReuniao.AddHours(4),
                    EventoId = model.EventoId,
                    Status = StatusEnum.Ativo
                };

                reuniaoRepository.Insert(reuniao);
            }

            reuniaoRepository.Save();
        }
    }
}
