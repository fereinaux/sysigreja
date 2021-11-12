using Core.Models.Eventos;
using Data.Entities;
using Data.Repository;
using System.Linq;
using System.Data.Entity;
using Utils.Enums;

namespace Core.Business.Eventos
{
    public class EventosBusiness : IEventosBusiness
    {
        private readonly IGenericRepository<Evento> eventoRepository;        

        public EventosBusiness(IGenericRepository<Evento> eventoRepository)
        {
            this.eventoRepository = eventoRepository;            
        }

        public void DeleteEvento(int id)
        {
            eventoRepository.Delete(id);
            eventoRepository.Save();
        }

        public Evento GetEventoAtivo()
        {
            return eventoRepository.GetAll().FirstOrDefault(e => e.Status == StatusEnum.Aberto);
        }

        public Evento GetEventoById(int id)
        {
            return eventoRepository.GetById(id);
        }

        public IQueryable<Evento> GetEventos()
        {
            return eventoRepository.GetAll();
        }

        public void PostEvento(PostEventoModel model)
        {
            Evento evento = null;

            if (model.Id > 0)
            {
                evento = eventoRepository.GetById(model.Id);

                evento.DataEvento = model.DataEvento.AddHours(5);
                evento.Capacidade = model.Capacidade;
                evento.TipoEvento = (TiposEventoEnum)model.TipoEvento;
                evento.Numeracao = model.Numeracao;
                evento.Valor = model.Valor;

                eventoRepository.Update(evento);
            }
            else
            {
                evento = new Evento
                {
                    DataEvento = model.DataEvento.AddHours(5),
                    Numeracao = model.Numeracao,
                    Capacidade = model.Capacidade,
                    Valor = model.Valor,
                    TipoEvento = (TiposEventoEnum)model.TipoEvento,
                    Status = GetEventoAtivo() != null ?
                    StatusEnum.Encerrado :
                    StatusEnum.Aberto
                };

                eventoRepository.Insert(evento);
            }

            eventoRepository.Save();
        }

        public bool ToggleEventoStatus(int id)
        {
            Evento evento = eventoRepository.GetById(id);

            StatusEnum status = evento.Status == StatusEnum.Aberto ?
                StatusEnum.Encerrado :
                StatusEnum.Aberto;

            if (GetEventoAtivo() != null &&
                status == StatusEnum.Aberto)
                return false;

            evento.Status = status;

            eventoRepository.Update(evento);

            eventoRepository.Save();

            return true;
        }
    }
}
