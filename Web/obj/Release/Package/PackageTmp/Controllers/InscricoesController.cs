
using Core.Business.Configuracao;
using Core.Business.ContaBancaria;
using Core.Business.Eventos;
using Core.Business.Lancamento;
using Core.Business.MeioPagamento;
using Core.Business.Newsletter;
using Core.Business.Participantes;
using Core.Models.Participantes;
using Data.Entities;
using SysIgreja.ViewModels;
using System;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using Utils.Enums;
using Utils.Extensions;

namespace SysIgreja.Controllers
{
    public class InscricoesController : Controller
    {
        private readonly IParticipantesBusiness participantesBusiness;
        private readonly IConfiguracaoBusiness configuracaoBusiness;
        private readonly ILancamentoBusiness lancamentoBusiness;
        private readonly IMeioPagamentoBusiness meioPagamentoBusiness;
        private readonly IContaBancariaBusiness contaBancariaBusiness;
        private readonly IEventosBusiness eventosBusiness;
        private readonly INewsletterBusiness newsletterBusiness;

        public InscricoesController(IParticipantesBusiness participantesBusiness, IConfiguracaoBusiness configuracaoBusiness, IContaBancariaBusiness contaBancariaBusiness, IEventosBusiness eventosBusiness, INewsletterBusiness newsletterBusiness, ILancamentoBusiness lancamentoBusiness, IMeioPagamentoBusiness meioPagamentoBusiness)
        {
            this.participantesBusiness = participantesBusiness;
            this.configuracaoBusiness = configuracaoBusiness;
            this.meioPagamentoBusiness = meioPagamentoBusiness;
            this.lancamentoBusiness = lancamentoBusiness;
            this.contaBancariaBusiness = contaBancariaBusiness;
            this.eventosBusiness = eventosBusiness;
            this.newsletterBusiness = newsletterBusiness;
        }

        public ActionResult Index()
        {

            ViewBag.Configuracao = configuracaoBusiness.GetConfiguracao();
            ViewBag.Campos = configuracaoBusiness.GetCampos().Select(x => x.Campo).ToList();
            ViewBag.Title = "Inscrições";
            var evento = eventosBusiness.GetEventoAtivo();
            if (evento == null)
                return RedirectToAction("InscricoesEncerradas");
            return View();
        }

        public ActionResult Equipe()
        {
            ViewBag.Title = "Inscrições";
            ViewBag.Configuracao = configuracaoBusiness.GetConfiguracao();
            var evento = eventosBusiness.GetEventoAtivo();
            if (evento == null)
                return RedirectToAction("InscricoesEncerradas");
            return View();
        }

        private bool CapacidadeUltrapassada(Evento evento, StatusEnum[] arrStatus)
        {
            return participantesBusiness
                            .GetParticipantesByEvento(evento.Id)
                            .Where(x => (arrStatus).Contains(x.Status))
                            .Count() >= evento.Capacidade;
        }

        public ActionResult InscricaoConcluida(int Id)
        {
            Participante participante = participantesBusiness.GetParticipanteById(Id);
            ViewBag.Configuracao = configuracaoBusiness.GetConfiguracao();
            ViewBag.Participante = new InscricaoConcluidaViewModel
            {
                Id = participante.Id,
                Apelido = participante.Apelido,
                Logo = participante.Evento.TipoEvento.GetNickname() + ".png",
                Evento = $"{participante.Evento.Numeracao.ToString()}º {participante.Evento.TipoEvento.GetDescription()}",
                Valor = participante.Evento.Valor.ToString("C", CultureInfo.CreateSpecificCulture("pt-BR")),
                DataEvento = participante.Evento.DataEvento.ToString("dd/MM/yyyy"),
                PadrinhoFone = participante.Padrinho.Fone,
                PadrinhoNome = participante.Padrinho.Nome
            };

            ViewBag.ContasBancarias = contaBancariaBusiness.GetContasBancarias().ToList()
               .Select(x => new ContaBancariaViewModel
               {
                   Id = x.Id,
                   Banco = x.Banco.GetDescription(),
                   Agencia = x.Agencia,
                   CPF = x.CPF,
                   Conta = x.Conta,
                   Nome = x.Nome,
                   Operacao = x.Operacao
               });


            if (participante.Status == StatusEnum.Inscrito)
            {

                return View("InscricaoConcluida");
            }
            else
                return View("InscricaoCompleta");
        }

        public ActionResult InscricaoEspera(int Id)
        {
            Participante participante = participantesBusiness.GetParticipanteById(Id);
            ViewBag.Configuracao = configuracaoBusiness.GetConfiguracao();
            ViewBag.Participante = new InscricaoConcluidaViewModel
            {
                Id = participante.Id,
                Apelido = participante.Apelido,
                Logo = participante.Evento.TipoEvento.GetNickname() + ".png",
                Evento = $"{participante.Evento.Numeracao.ToString()}º {participante.Evento.TipoEvento.GetDescription()}",
                Valor = participante.Evento.Valor.ToString("C", CultureInfo.CreateSpecificCulture("pt-BR")),
                DataEvento = participante.Evento.DataEvento.ToString("dd/MM/yyyy")
            };


            return View("InscricaoEspera");
        }

        public ActionResult InscricoesEncerradas()
        {
            ViewBag.Configuracao = configuracaoBusiness.GetConfiguracao();
            return View();
        }

        [HttpPost]
        public ActionResult PostInscricao(PostInscricaoModel model)
        {
            var evento = eventosBusiness.GetEventoAtivo();
            model.EventoId = model.EventoId > 0 ? model.EventoId : evento.Id;

            if (evento != null && participantesBusiness.GetParticipantesByEvento(model.EventoId).Where(x => x.Status != StatusEnum.Cancelado).Count() >= evento.Capacidade)
            {
                model.Status = "Espera";

                return Json(Url.Action("InscricaoEspera", new { Id = participantesBusiness.PostInscricao(model) }));
            }

            return Json(Url.Action("InscricaoConcluida", new { Id = participantesBusiness.PostInscricao(model) }));
        }

        [HttpPost]
        public ActionResult Checkin(PostInscricaoModel model)
        {
            return Json(new { Id = participantesBusiness.PostInscricao(model) });
        }

        [HttpPost]
        public ActionResult VerificaCadastro(string Email)
        {
            var participante = participantesBusiness.GetParticipantesByEvento(eventosBusiness.GetEventoAtivo().Id).FirstOrDefault(x => x.Email == Email && (new StatusEnum[] { StatusEnum.Confirmado, StatusEnum.Inscrito }).Contains(x.Status));

            if (participante != null)
                return Json(Url.Action("InscricaoConcluida", new { Id = participante.Id }));

            var participanteConsulta = participantesBusiness.GetParticipanteConsulta(Email);

            if (participanteConsulta != null)
                return Json(new { Participante = participanteConsulta }, JsonRequestBehavior.AllowGet);

            return new HttpStatusCodeResult(200);
        }


    }
}