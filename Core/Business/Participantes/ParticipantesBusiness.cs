using Core.Business.Circulos;
using Core.Business.Eventos;
using Core.Business.Quartos;
using Core.Models.Participantes;
using Data.Entities;
using Data.Repository;
using System.Linq;
using System.Data.Entity;
using Utils.Enums;
using System;
using Data.Context;

namespace Core.Business.Participantes
{
    public class ParticipantesBusiness : IParticipantesBusiness
    {
        private readonly IGenericRepository<Participante> participanteRepository;
        private readonly IGenericRepositoryConsulta<ParticipanteConsulta> participanteConsultaRepository;
        private readonly IGenericRepository<EquipanteEvento> equipanteEventoRepository;
        private readonly IEventosBusiness eventosBusiness;
        private readonly ICirculosBusiness circulosBusiness;
        private readonly IQuartosBusiness quartosBusiness;

        public ParticipantesBusiness(IGenericRepository<Participante> participanteRepository, IGenericRepositoryConsulta<ParticipanteConsulta> participanteConsultaRepository, IQuartosBusiness quartosBusiness, IEventosBusiness eventosBusiness, ICirculosBusiness circulosBusiness, IGenericRepository<EquipanteEvento> equipanteEventoRepository)
        {
            this.participanteRepository = participanteRepository;
            this.participanteConsultaRepository = participanteConsultaRepository;
            this.equipanteEventoRepository = equipanteEventoRepository;
            this.eventosBusiness = eventosBusiness;
            this.quartosBusiness = quartosBusiness;
            this.circulosBusiness = circulosBusiness;
        }

        public void CancelarInscricao(int id)
        {
            Participante participante = participanteRepository.GetById(id);
            participante.Status = StatusEnum.Cancelado;
            circulosBusiness.ChangeCirculo(id, null);
            quartosBusiness.ChangeQuarto(id, null);

            var emEspera = participanteRepository.GetAll().Where(x => x.Status == StatusEnum.Espera).OrderBy(x => x.Id).FirstOrDefault();

            if (emEspera != null && participanteRepository.GetAll().Where(x => x.Status == StatusEnum.Confirmado || x.Status == StatusEnum.Inscrito).Count() - 1 < eventosBusiness.GetEventoById(participante.EventoId).Capacidade)
            {                
                emEspera.Status = StatusEnum.Inscrito;
                participanteRepository.Update(emEspera);
            }

            participanteRepository.Update(participante);
            participanteRepository.Save();
        }

        public Data.Entities.Participante GetParticipanteById(int id)
        {
            return participanteRepository.GetAll(x => x.Id == id).Include(x => x.Evento).SingleOrDefault();
        }

        public Participante GetParticipanteByReference(string reference)
        {
            return participanteRepository.GetAll(x => x.ReferenciaPagSeguro == reference).Include(x => x.Evento).SingleOrDefault();
        }

        public IQueryable<Participante> GetParticipantes()
        {
            return participanteRepository.GetAll().Include(x => x.Evento);
        }

        private ParticipanteConsulta MapUpdateParticipanteConsulta(ParticipanteConsulta entity, PostInscricaoModel model)
        {

            entity.Nome = model.Nome;
            entity.Apelido = model.Apelido;
            entity.DataNascimento = model.DataNascimento.AddHours(5);
            entity.Fone = model.Fone;
            entity.Email = model.Email;            
            entity.Sexo = model.Sexo;
            return entity;

        }


        public int PostInscricao(PostInscricaoModel model)
        {
            Participante participante = null;
            if (model.Id > 0)
            {
                participante = MapUpdateParticipante(model);
                participanteRepository.Update(participante);
            }
            else
            {
                participante = MapCreateParticipante(model);
                participanteRepository.Insert(participante);
            }

            var participanteConsulta = GetParticipanteConsulta(model.Email);
            if (participanteConsulta != null)
            {
                participanteConsultaRepository.Update(MapUpdateParticipanteConsulta(participanteConsulta, model));
            }
            else
            {
                var participanteConsultaModel = MapCreateParticipanteConsulta(model);
                participanteConsultaRepository.Insert(participanteConsultaModel);
            }

            participanteConsultaRepository.Save();
            participanteRepository.Save();

            CheckIn(participante, model.CancelarCheckin);

            return participante.Id;
        }

        private void CheckIn(Participante participante, bool cancelarCheckin)
        {
            if (participante.Checkin)
            {
                ManageCirculo(participante);
                ManageQuarto(participante);
            }
            else
            {
                if (cancelarCheckin)
                {
                    circulosBusiness.ChangeCirculo(participante.Id, null);
                    quartosBusiness.ChangeQuarto(participante.Id, null);
                }
            }
        }

        private void ManageQuarto(Participante participante)
        {
            if (!quartosBusiness
                              .GetQuartosComParticipantes(participante.EventoId)
                              .Where(x => x.ParticipanteId == participante.Id)
                              .Any())
            {
                var quarto = quartosBusiness.GetNextQuarto(participante.EventoId, participante.Sexo);
                if (quarto != null)
                    quartosBusiness.ChangeQuarto(participante.Id, quarto.Id);
            }
        }

        private void ManageCirculo(Participante participante)
        {
            if (!circulosBusiness
                                .GetCirculosComParticipantes(participante.EventoId)
                                .Where(x => x.ParticipanteId == participante.Id)
                                .Any())
            {
                var circulo = circulosBusiness.GetNextCirculo(participante.EventoId);
                if (circulo != null)
                    circulosBusiness.ChangeCirculo(participante.Id, circulo.Id);
            }
        }

        private Participante MapUpdateParticipante(PostInscricaoModel model)
        {
            Participante participante = participanteRepository.GetById(model.Id);
            participante.Nome = model.Nome;
            participante.Apelido = model.Apelido;
            participante.DataNascimento = model.DataNascimento.AddHours(5);
            participante.Fone = model.Fone;
            participante.Email = model.Email;
            participante.Logradouro = model.Logradouro;
            participante.Complemento = model.Complemento;
            participante.Bairro = model.Bairro;
            participante.NomePai = model.NomePai;
            participante.FonePai = model.FonePai;
            participante.NomeMae = model.NomeMae;
            participante.FoneMae = model.FoneMae;
            participante.NomeConvite = model.NomeConvite;
            participante.FoneConvite = model.FoneConvite;
            participante.Sexo = model.Sexo;
            participante.HasAlergia = model.HasAlergia;
            participante.Alergia = model.HasAlergia ? model.Alergia : null;
            participante.HasMedicacao = model.HasMedicacao;
            participante.Medicacao = model.HasMedicacao ? model.Medicacao : null;
            participante.HasRestricaoAlimentar = model.HasRestricaoAlimentar;
            participante.RestricaoAlimentar = model.HasRestricaoAlimentar ? model.RestricaoAlimentar : null;
            participante.Checkin = model.Checkin;
            return participante;
        }

        private Equipante getNextPadrinho(int eventoid)
        {
            var query = equipanteEventoRepository
                 .GetAll(x => x.EventoId == eventoid && x.Equipe == EquipesEnum.Secretaria)
                 .Include(x => x.Equipante)
                 .ToList()
                 .Select(x => new
                 {
                     Equipante = x,
                     Qtd = participanteRepository.GetAll(y => y.PadrinhoId == x.EquipanteId && (y.Status == StatusEnum.Confirmado || y.Status == StatusEnum.Inscrito)).Count()
                 })
                 .ToList();

            return query.Any() ? query.OrderBy(x => x.Qtd).FirstOrDefault().Equipante.Equipante : null;

        }

        private Participante MapCreateParticipante(PostInscricaoModel model)
        {
            return new Participante
            {
                Nome = model.Nome,
                Apelido = model.Apelido,
                DataNascimento = model.DataNascimento.AddHours(5),
                Fone = model.Fone,
                Email = model.Email,
                Logradouro = model.Logradouro,
                Complemento = model.Complemento,
                Bairro = model.Bairro,
                NomePai = model.NomePai,
                FonePai = model.FonePai,
                NomeMae = model.NomeMae,
                FoneMae = model.FoneMae,
                NomeConvite = model.NomeConvite,
                FoneConvite = model.FoneConvite,
                ReferenciaPagSeguro = Guid.NewGuid().ToString(),
                Sexo = model.Sexo,
                Status = model.Status == "Espera" ? StatusEnum.Espera : StatusEnum.Inscrito,
                HasAlergia = model.HasAlergia,
                Alergia = model.HasAlergia ? model.Alergia : null,
                HasMedicacao = model.HasMedicacao,
                Medicacao = model.HasMedicacao ? model.Medicacao : null,
                HasRestricaoAlimentar = model.HasRestricaoAlimentar,
                RestricaoAlimentar = model.HasRestricaoAlimentar ? model.RestricaoAlimentar : null,
                EventoId = model.EventoId,
                PendenciaContato = false,
                Boleto = false,
                PendenciaBoleto = false,
                Checkin = model.Checkin,
                PadrinhoId = getNextPadrinho(model.EventoId)?.Id
            };
        }


        private ParticipanteConsulta MapCreateParticipanteConsulta(PostInscricaoModel model)
        {
            return new ParticipanteConsulta
            {
                Nome = model.Nome,
                Apelido = model.Apelido,
                DataNascimento = model.DataNascimento.AddHours(5),
                Fone = model.Fone,
                Email = model.Email,
                Logradouro = model.Logradouro,
                Complemento = model.Complemento,
                Bairro = model.Bairro,
                NomePai = model.NomePai,
                FonePai = model.FonePai,
                NomeMae = model.NomeMae,
                FoneMae = model.FoneMae,
                Sexo = model.Sexo,
                HasAlergia = model.HasAlergia,
                Alergia = model.HasAlergia ? model.Alergia : null,
                HasMedicacao = model.HasMedicacao,
                Medicacao = model.HasMedicacao ? model.Medicacao : null,
                HasRestricaoAlimentar = model.HasRestricaoAlimentar,
                RestricaoAlimentar = model.HasRestricaoAlimentar ? model.RestricaoAlimentar : null,
            };
        }

        public IQueryable<Participante> GetParticipantesByEvento(int eventoId)
        {
            return participanteRepository.GetAll(x => x.EventoId == eventoId).Include(x => x.Evento).Include(x => x.Padrinho).Include(x => x.Arquivos).Include(x => x.Circulos).Include(x => x.Circulos.Select(y => y.Circulo));
        }

        public void TogglePendenciaContato(int id)
        {
            var participante = GetParticipanteById(id);
            participante.PendenciaContato = !participante.PendenciaContato;
            participanteRepository.Update(participante);
            participanteRepository.Save();
        }

        public void TogglePendenciaBoleto(int id)
        {
            var participante = GetParticipanteById(id);
            participante.PendenciaBoleto = !participante.PendenciaBoleto;
            participanteRepository.Update(participante);
            participanteRepository.Save();
        }

        public void SolicitarBoleto(int id)
        {
            var participante = GetParticipanteById(id);
            participante.PendenciaBoleto = false;
            participante.Boleto = true;
            participanteRepository.Update(participante);
            participanteRepository.Save();
        }

        public IQueryable<Participante> GetAniversariantesByEvento(int eventoId)
        {
            var data = eventosBusiness.GetEventoById(eventoId).DataEvento;

            return participanteRepository.GetAll(x => x.Status != StatusEnum.Cancelado && x.EventoId == eventoId && x.DataNascimento.Month == data.Month);
        }

        public IQueryable<Participante> GetRestricoesByEvento(int eventoId)
        {
            return participanteRepository.GetAll(x => x.Status != StatusEnum.Cancelado && x.EventoId == eventoId && x.HasRestricaoAlimentar);
        }

        public void ToggleSexo(int id)
        {
            var participante = GetParticipanteById(id);
            participante.Sexo = participante.Sexo == SexoEnum.Feminino ? SexoEnum.Masculino : SexoEnum.Feminino;
            participanteRepository.Update(participante);
            participanteRepository.Save();
        }

        public IQueryable<Participante> GetParentesByEvento(int eventoId)
        {
            return participanteRepository.GetAll(x => x.Status != StatusEnum.Cancelado && x.EventoId == eventoId && x.HasParente.HasValue && x.HasParente.Value);
        }

        public void ToggleVacina(int id)
        {
            var participante = GetParticipanteById(id);
            participante.HasVacina = !participante.HasVacina;
            participanteRepository.Update(participante);
            participanteRepository.Save();
        }

        public void ToggleTeste(int id)
        {
            var participante = GetParticipanteById(id);
            participante.HasTeste = !participante.HasTeste;
            participanteRepository.Update(participante);
            participanteRepository.Save();
        }

        public void ToggleCheckin(int id)
        {
            var participante = GetParticipanteById(id);
            participante.Checkin = !participante.Checkin;
            participanteRepository.Update(participante);
            participanteRepository.Save();

            if (participante.Checkin)
            {
                ManageCirculo(participante);
                ManageQuarto(participante);
            }

        }


        public void PostInfo(PostInfoModel model)
        {
            var participante = GetParticipanteById(model.Id);
            participante.Observacao = model.Observacao;
            participante.MsgGeral = model.MsgGeral;
            participante.MsgVacina = model.MsgVacina;
            participante.MsgPagamento = model.MsgPagamento;
            participante.MsgFoto = model.MsgFoto;
            participante.MsgNoitita = model.MsgNoitita;
            participanteRepository.Update(participante);
            participanteRepository.Save();
        }

        public ParticipanteConsulta GetParticipanteConsulta(string email)
        {
            return participanteConsultaRepository.GetAll(x => x.Email == email).FirstOrDefault();
        }
    }
}
