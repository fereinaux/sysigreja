using Arquitetura.Controller;
using AutoMapper;
using Core.Business.Account;
using Core.Business.Configuracao;
using Core.Business.Equipes;
using Core.Business.Eventos;
using Core.Business.Quartos;
using Core.Models.Quartos;
using SysIgreja.ViewModels;
using System.Linq;
using System.Web.Mvc;
using Utils.Constants;
using Utils.Enums;
using Utils.Extensions;
using Utils.Services;

namespace SysIgreja.Controllers
{

    [Authorize(Roles = Usuario.Master + "," + Usuario.Admin + "," + Usuario.Secretaria)]
    public class QuartoController : SysIgrejaControllerBase
    {
        private readonly IQuartosBusiness quartosBusiness;
        private readonly IEquipesBusiness equipesBusiness;
        private readonly IMapper mapper;

        public QuartoController(IQuartosBusiness quartosBusiness, IEquipesBusiness equipesBusiness, IEventosBusiness eventosBusiness, IAccountBusiness accountBusiness, IConfiguracaoBusiness configuracaoBusiness) : base(eventosBusiness, accountBusiness, configuracaoBusiness)
        {
            this.quartosBusiness = quartosBusiness;
            this.equipesBusiness = equipesBusiness;
            mapper = new MapperRealidade().mapper;
        }

        public ActionResult Index()
        {
            ViewBag.Title = "Quartos";
            GetEventos();

            return View();
        }

        public ActionResult QuartoEquipe()
        {
            ViewBag.Title = "Quartos da Equipe";
            GetEventos();

            return View();
        }

        [HttpPost]
        public ActionResult GetQuartos(int EventoId, TipoPessoaEnum? tipo)
        {
            var result = quartosBusiness
                .GetQuartos()
                .Where(x => x.EventoId == EventoId && x.TipoPessoa == (tipo ?? TipoPessoaEnum.Participante))
                .ToList()
                .Select(x => new QuartoViewModel
                {
                    Id = x.Id,
                    Capacidade = $"{quartosBusiness.GetParticipantesByQuartos(x.Id, tipo).Count().ToString()}/{x.Capacidade.ToString()}",
                    Titulo = x.Titulo,
                    Sexo = x.Sexo.GetDescription()
                });

            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetQuarto(int Id)
        {
            var result = quartosBusiness.GetQuartoById(Id);

            return Json(new { Quarto = mapper.Map<PostQuartoModel>(result) }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult PostQuarto(PostQuartoModel model)
        {
            quartosBusiness.PostQuarto(model);

            return new HttpStatusCodeResult(200);
        }

        [HttpPost]
        public ActionResult DeleteQuarto(int Id)
        {
            quartosBusiness.DeleteQuarto(Id);

            return new HttpStatusCodeResult(200);
        }

        [HttpPost]
        public ActionResult DistribuirQuartos(int EventoId, TipoPessoaEnum? tipo)
        {
            quartosBusiness.DistribuirQuartos(EventoId, tipo ?? TipoPessoaEnum.Participante);

            return new HttpStatusCodeResult(200);
        }

        [HttpGet]
        public ActionResult GetParticipantesSemQuarto(int EventoId, TipoPessoaEnum? tipo)
        {


            return Json(new
            {
                Participantes = tipo == TipoPessoaEnum.Equipante ? quartosBusiness.GetEquipantesSemQuarto(EventoId).Select(x => new
                {
                    Id = x.Id,
                    Nome = x.Nome
                }).ToList() : quartosBusiness.GetParticipantesSemQuarto(EventoId).Select(x => new
                {
                    Id = x.Id,
                    Nome = x.Nome
                }).ToList()
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetQuartosComParticipantes(int EventoId, TipoPessoaEnum? tipo)
        {
            if (tipo == TipoPessoaEnum.Equipante)
            {
                return Json(new
                {
                    Quartos = quartosBusiness.GetQuartosComParticipantes(EventoId, TipoPessoaEnum.Equipante).ToList().Select(x => new
                    {
                        Nome = UtilServices.CapitalizarNome(x.Equipante.Nome),
                        ParticipanteId = x.EquipanteId,
                        QuartoId = x.QuartoId,
                        Sexo = x.Quarto.Sexo.GetDescription(),
                        Capacidade = $"{quartosBusiness.GetParticipantesByQuartos(x.QuartoId, TipoPessoaEnum.Equipante).Count().ToString()}/{x.Quarto.Capacidade.ToString()}",
                    }).ToList()
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {

                return Json(new
                {
                    Quartos = quartosBusiness.GetQuartosComParticipantes(EventoId, TipoPessoaEnum.Participante).ToList().Select(x => new
                    {
                        Nome = UtilServices.CapitalizarNome(x.Participante.Nome),
                        ParticipanteId = x.ParticipanteId,
                        QuartoId = x.QuartoId,
                        Sexo = x.Quarto.Sexo.GetDescription(),
                        Capacidade = $"{quartosBusiness.GetParticipantesByQuartos(x.QuartoId, TipoPessoaEnum.Participante).Count().ToString()}/{x.Quarto.Capacidade.ToString()}",
                    }).ToList()
                }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult ChangeQuarto(int ParticipanteId, int? DestinoId, TipoPessoaEnum? tipo)
        {
            var mensagem = quartosBusiness.ChangeQuarto(ParticipanteId, DestinoId, tipo);
            if (mensagem == "OK")
            {
                return new HttpStatusCodeResult(200);
            }

            return new HttpStatusCodeResult(400, mensagem);
        }

        [HttpGet]
        public ActionResult GetEquipantesByQuarto(int QuartoId)
        {
            var result = quartosBusiness.GetParticipantesByQuartos(QuartoId, TipoPessoaEnum.Equipante).ToList().Select(x => new
            {
                Nome = UtilServices.CapitalizarNome(x.Equipante.Nome),
                Apelido = UtilServices.CapitalizarNome(x.Equipante.Apelido),
            });

            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }
    }
}