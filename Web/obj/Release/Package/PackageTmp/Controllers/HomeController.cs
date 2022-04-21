using Arquitetura.Controller;
using Core.Business.Account;
using Core.Business.Arquivos;
using Core.Business.Configuracao;
using Core.Business.Equipes;
using Core.Business.Eventos;
using Core.Business.Lancamento;
using Core.Business.Participantes;
using Core.Business.Reunioes;
using SysIgreja.ViewModels;
using System;
using System.Linq;
using System.Web.Mvc;
using Utils.Constants;
using Utils.Enums;
using Utils.Extensions;
using Utils.Services;

namespace SysIgreja.Controllers
{

    [Authorize]
    public class HomeController : SysIgrejaControllerBase
    {
        private readonly IEquipesBusiness equipesBusiness;
        private readonly IParticipantesBusiness participantesBusiness;
        private readonly ILancamentoBusiness lancamentoBusiness;
        private readonly IReunioesBusiness reunioesBusiness;
        private readonly IEventosBusiness eventosBusiness;
        private readonly IArquivosBusiness arquivosBusiness;
        private readonly IAccountBusiness accountBusiness;

        public HomeController(IEquipesBusiness equipesBusiness, IParticipantesBusiness participantesBusiness, IArquivosBusiness arquivosBusiness, ILancamentoBusiness lancamentoBusiness, IEventosBusiness eventosBusiness, IAccountBusiness accountBusiness, IReunioesBusiness reunioesBusiness, IConfiguracaoBusiness configuracaoBusiness) : base(eventosBusiness, accountBusiness, configuracaoBusiness)
        {
            this.lancamentoBusiness = lancamentoBusiness;
            this.participantesBusiness = participantesBusiness;
            this.equipesBusiness = equipesBusiness;
            this.eventosBusiness = eventosBusiness;
            this.accountBusiness = accountBusiness;
            this.reunioesBusiness = reunioesBusiness;
            this.arquivosBusiness = arquivosBusiness;
        }

        [Authorize(Roles = Usuario.Master + "," + Usuario.Admin + "," + Usuario.Secretaria)]
        public ActionResult Admin()
        {
            ViewBag.Title = "Sistema de Gestão";

            GetEventos();
            return View();
        }

        [HttpGet]
        public ActionResult GetResultadosAdmin(int EventoId)
        {
            var ReceberPix = lancamentoBusiness.GetPagamentosEvento(EventoId).Where(x => x.Tipo == TiposLancamentoEnum.Receber && x.MeioPagamento.Descricao == "PIX").Select(x => x.Valor).DefaultIfEmpty(0).Sum();
            var PagarPix = lancamentoBusiness.GetPagamentosEvento(EventoId).Where(x => x.Tipo == TiposLancamentoEnum.Pagar && x.MeioPagamento.Descricao == "PIX").Select(x => x.Valor).DefaultIfEmpty(0).Sum();
            var SaldoPix = ReceberPix - PagarPix;
            var ReceberDinheiro = lancamentoBusiness.GetPagamentosEvento(EventoId).Where(x => x.Tipo == TiposLancamentoEnum.Receber && x.MeioPagamento.Descricao == "Dinheiro").Select(x => x.Valor).DefaultIfEmpty(0).Sum();
            var PagarDinheiro = lancamentoBusiness.GetPagamentosEvento(EventoId).Where(x => x.Tipo == TiposLancamentoEnum.Pagar && x.MeioPagamento.Descricao == "Dinheiro").Select(x => x.Valor).DefaultIfEmpty(0).Sum();
            var SaldoDinheiro = ReceberDinheiro - PagarDinheiro;
            var TotalReceber = lancamentoBusiness.GetPagamentosEvento(EventoId).Where(x => x.Tipo == TiposLancamentoEnum.Receber).Select(x => x.Valor).DefaultIfEmpty(0).Sum();
            var TotalPagar = lancamentoBusiness.GetPagamentosEvento(EventoId).Where(x => x.Tipo == TiposLancamentoEnum.Pagar).Select(x => x.Valor).DefaultIfEmpty(0).Sum();
            var SaldoGeral = TotalReceber - TotalPagar;
            var result = new
            {
                Evento = eventosBusiness.GetEventoById(EventoId).Status.GetDescription(),
                Confirmados = participantesBusiness.GetParticipantesByEvento(EventoId).Where(x => x.Status == StatusEnum.Confirmado).Count(),
                Cancelados = participantesBusiness.GetParticipantesByEvento(EventoId).Where(x => x.Status == StatusEnum.Cancelado).Count(),
                Espera = participantesBusiness.GetParticipantesByEvento(EventoId).Where(x => x.Status == StatusEnum.Espera).Count(),
                Presentes = participantesBusiness.GetParticipantesByEvento(EventoId).Where(x => x.Checkin).Count(),
                Isencoes = lancamentoBusiness.GetPagamentosEvento(EventoId).ToList().Where(x => x.ParticipanteId != null && x.Tipo == TiposLancamentoEnum.Receber && x.MeioPagamento.Descricao == MeioPagamentoPadraoEnum.Isencao.GetDescription()).Count(),
                Total = participantesBusiness.GetParticipantesByEvento(EventoId).Where(x => x.Status != StatusEnum.Cancelado && x.Status != StatusEnum.Espera).Count(),
                Boletos = participantesBusiness.GetParticipantesByEvento(EventoId).Where(x => x.Boleto && !x.PendenciaBoleto).Count(),
                Contatos = participantesBusiness.GetParticipantesByEvento(EventoId).Where(x => !x.PendenciaContato).Count(),
                Meninos = participantesBusiness.GetParticipantesByEvento(EventoId).Where(x => x.Sexo == SexoEnum.Masculino && x.Status != StatusEnum.Cancelado && x.Status != StatusEnum.Espera).Count(),
                Meninas = participantesBusiness.GetParticipantesByEvento(EventoId).Where(x => x.Sexo == SexoEnum.Feminino && x.Status != StatusEnum.Cancelado && x.Status != StatusEnum.Espera).Count(),
                SaldoGeral = UtilServices.DecimalToMoeda(SaldoGeral),
                SaldoPix = UtilServices.DecimalToMoeda(SaldoPix),
                SaldoDinheir = UtilServices.DecimalToMoeda(SaldoDinheiro),
                TotalPagar = UtilServices.DecimalToMoeda(TotalPagar),
                TotalReceber = UtilServices.DecimalToMoeda(TotalReceber),
                UltimosInscritos = participantesBusiness.GetParticipantesByEvento(EventoId).Where(x => x.Status != StatusEnum.Cancelado)
                .OrderByDescending(x => x.DataCadastro).Take(5).ToList().Select(x => new ParticipanteViewModel
                {
                    Nome = UtilServices.CapitalizarNome(x.Nome),
                    Sexo = x.Sexo.GetDescription(),
                    Idade = UtilServices.GetAge(x.DataNascimento)
                }).ToList(),
                EquipeMeninos = equipesBusiness.GetEquipantesEvento(EventoId).Where(x => x.Equipante.Sexo == SexoEnum.Masculino).Count(),
                EquipeMeninas = equipesBusiness.GetEquipantesEvento(EventoId).Where(x => x.Equipante.Sexo == SexoEnum.Feminino).Count(),
                Equipes = equipesBusiness.GetEquipes(EventoId).Select(x => new ListaEquipesViewModel
                {
                    Id = x.Id,
                    Equipe = x.Description,
                    QuantidadeMembros = equipesBusiness.GetMembrosEquipe(EventoId, (EquipesEnum)x.Id).Count()
                }).ToList(),
                Reunioes = reunioesBusiness.GetReunioes(EventoId).ToList().Select(x => new ReuniaoViewModel
                {
                    Id = x.Id,
                    DataReuniao = x.DataReuniao,
                    Presenca = x.Presenca.Count()
                }).ToList()
            };

            return Json(new { result }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetDetalhamentoEvento(int EventoId)
        {
            var result = new
            {
                Equipantes = equipesBusiness
                .GetEquipantesEvento(EventoId)
                .OrderBy(x => x.Equipe)
                .ThenBy(x => x.Tipo)
                .ThenBy(x => x.Equipante.Nome)
                .ToList()
                .Select(x => new
                {
                    Equipe = x.Equipe.GetDescription(),
                    Nome = x.Equipante.Nome,
                    Tipo = x.Tipo.GetDescription(),
                    Fone = x.Equipante.Fone
                })
            };


            return Json(new { result }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Coordenador()
        {
            ViewBag.Title = "Sistema de Gestão";
            int eventoId = (eventosBusiness.GetEventoAtivo() ?? eventosBusiness.GetEventos().OrderByDescending(x => x.DataEvento).First()).Id;
            var user = GetApplicationUser();
            var equipanteEvento = equipesBusiness.GetEquipanteEventoByUser(eventoId, user.Id);
            var membrosEquipe = equipesBusiness.GetMembrosEquipe(eventoId, equipanteEvento.Equipe);
            ViewBag.Equipante = equipanteEvento.Equipante;
            ViewBag.Equipe = equipanteEvento.Equipe.GetDescription();
            ViewBag.QtdMembros = membrosEquipe.Count();
            ViewBag.Reunioes = reunioesBusiness.GetReunioes(eventoId)
                .ToList()
                .OrderBy(x => DateTime.Now.AddHours(4).Subtract(x.DataReuniao).TotalDays < 0 ? DateTime.Now.AddHours(4).Subtract(x.DataReuniao).TotalDays * -1 : DateTime.Now.AddHours(4).Subtract(x.DataReuniao).TotalDays)
                .Select(x => new ReuniaoViewModel { DataReuniao = x.DataReuniao, Id = x.Id });
            ViewBag.Membros = membrosEquipe.ToList().Select(x => new EquipanteViewModel
            {
                Id = x.Equipante.Id,
                Sexo = x.Equipante.Sexo.GetDescription(),
                Fone = x.Equipante.Fone,
                Idade = UtilServices.GetAge(x.Equipante.DataNascimento),
                Nome = x.Equipante.Nome,
                Vacina = x.Equipante.HasVacina,
                Faltas = reunioesBusiness.GetFaltasByEquipanteId(x.EquipanteId, eventoId),
                Oferta = lancamentoBusiness.GetPagamentosEquipante(x.EquipanteId).Any(),
                Foto = x.Equipante.Arquivos.Any(y => y.IsFoto)
            });

            return View();
        }

        [HttpGet]
        public ActionResult GetPresenca(int ReuniaoId)
        {
            var presenca = equipesBusiness.GetPresenca(ReuniaoId).Select(x => x.EquipanteEventoId).ToList();

            var user = GetApplicationUser();
            var eventoId = (eventosBusiness.GetEventoAtivo() ?? eventosBusiness.GetEventos().OrderByDescending(x => x.DataEvento).First()).Id;

            var result = equipesBusiness
                .GetMembrosEquipe(eventoId, equipesBusiness.GetEquipanteEventoByUser(eventoId, user.Id).Equipe).ToList().Select(x => new PresencaViewModel
                {
                    Id = x.Id,
                    Nome = x.Equipante.Nome,
                    Presenca = presenca.Contains(x.Id)
                });

            return Json(new { result }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult TogglePresenca(int EquipanteEventoId, int ReuniaoId)
        {
            equipesBusiness.TogglePresenca(EquipanteEventoId, ReuniaoId);

            return new HttpStatusCodeResult(200);
        }

        public ActionResult Index()
        {
            var user = GetApplicationUser();

            switch (user.Perfil)
            {
                case PerfisUsuarioEnum.Master:
                case PerfisUsuarioEnum.Admin:
                case PerfisUsuarioEnum.Secretaria:
                    return RedirectToAction("Admin", "Home");
                case PerfisUsuarioEnum.Coordenador:
                    return RedirectToAction("Coordenador", "Home");
                default:
                    return RedirectToAction("Admin", "Home");
            }
        }
    }
}