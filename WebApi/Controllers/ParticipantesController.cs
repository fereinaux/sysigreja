using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Business.Account;
using Core.Business.Equipes;
using Core.Business.Eventos;
using Core.Business.Lancamento;
using Core.Business.Participantes;
using Core.Business.Reunioes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Utils.Enums;
using Utils.Extensions;
using Utils.Services;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParticipantesController : ControllerBase
    {
        private readonly IEquipesBusiness equipesBusiness;
        private readonly IParticipantesBusiness participantesBusiness;
        private readonly ILancamentoBusiness lancamentoBusiness;
        private readonly IReunioesBusiness reunioesBusiness;
        private readonly IEventosBusiness eventosBusiness;
        private readonly IAccountBusiness accountBusiness;

        public ParticipantesController(IEquipesBusiness equipesBusiness, IParticipantesBusiness participantesBusiness, ILancamentoBusiness lancamentoBusiness, IEventosBusiness eventosBusiness, IAccountBusiness accountBusiness, IReunioesBusiness reunioesBusiness)
        {
            this.lancamentoBusiness = lancamentoBusiness;
            this.participantesBusiness = participantesBusiness;
            this.equipesBusiness = equipesBusiness;
            this.eventosBusiness = eventosBusiness;
            this.accountBusiness = accountBusiness;
            this.reunioesBusiness = reunioesBusiness;
        }

        [HttpGet]
        public JsonResult GetResultadosAdmin()
        {
            int EventoId = 1;

            var result = new 
            {
                Evento = eventosBusiness.GetEventoById(EventoId).Status.GetDescription(),
                Confirmados = participantesBusiness.GetParticipantesByEvento(EventoId).Where(x => x.Status == StatusEnum.Confirmado).Count(),
                Cancelados = participantesBusiness.GetParticipantesByEvento(EventoId).Where(x => x.Status == StatusEnum.Cancelado).Count(),
                Presentes = participantesBusiness.GetParticipantesByEvento(EventoId).Where(x => x.Checkin).Count(),
                Isencoes = lancamentoBusiness.GetPagamentosEvento(EventoId).ToList().Where(x => x.ParticipanteId != null && x.Tipo == TiposLancamentoEnum.Receber && x.MeioPagamento.Descricao == MeioPagamentoPadraoEnum.Isencao.GetDescription()).Count(),
                Total = participantesBusiness.GetParticipantesByEvento(EventoId).Where(x => x.Status != StatusEnum.Cancelado).Count(),
                Boletos = participantesBusiness.GetParticipantesByEvento(EventoId).Where(x => x.Boleto && !x.PendenciaBoleto).Count(),
                Contatos = participantesBusiness.GetParticipantesByEvento(EventoId).Where(x => !x.PendenciaContato).Count(),
                Meninos = participantesBusiness.GetParticipantesByEvento(EventoId).Where(x => x.Sexo == SexoEnum.Masculino && x.Status != StatusEnum.Cancelado).Count(),
                Meninas = participantesBusiness.GetParticipantesByEvento(EventoId).Where(x => x.Sexo == SexoEnum.Feminino && x.Status != StatusEnum.Cancelado).Count(),
                TotalReceber = UtilServices.DecimalToMoeda(lancamentoBusiness.GetPagamentosEvento(EventoId).Where(x => x.Tipo == TiposLancamentoEnum.Receber).Select(x => x.Valor).DefaultIfEmpty(0).Sum()),
                TotalPagar = UtilServices.DecimalToMoeda(lancamentoBusiness.GetPagamentosEvento(EventoId).Where(x => x.Tipo == TiposLancamentoEnum.Pagar).Select(x => x.Valor).DefaultIfEmpty(0).Sum()),
                UltimosInscritos = participantesBusiness.GetParticipantesByEvento(EventoId)
                 .OrderByDescending(x => x.DataCadastro).Take(5).ToList().Select(x => new 
                 {
                     Nome = UtilServices.CapitalizarNome(x.Nome),
                     Sexo = x.Sexo.GetDescription(),
                     Idade = UtilServices.GetAge(x.DataNascimento)
                 }).ToList(),
                Equipes = equipesBusiness.GetEquipes(EventoId).Select(x => new 
                {
                    Id = x.Id,
                    Equipe = x.Description,
                    QuantidadeMembros = equipesBusiness.GetMembrosEquipe(EventoId, (EquipesEnum)x.Id).Count()
                }).ToList(),
                Reunioes = reunioesBusiness.GetReunioes(EventoId).ToList().Select(x => new 
                {
                    Id = x.Id,
                    DataReuniao = x.DataReuniao,
                    Presenca = x.Presenca.Count()
                }).ToList()
            };

            return new JsonResult(result);
        }      
    }
}
