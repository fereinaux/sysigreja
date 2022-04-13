using Arquitetura.Controller;
using Core.Business.Account;
using Core.Business.Quartos;
using Core.Business.Equipes;
using Core.Business.Eventos;
using Core.Business.Reunioes;
using Core.Models.Quartos;
using Core.Models.Reunioes;
using SysIgreja.ViewModels;
using System.Linq;
using System.Web.Mvc;
using Utils.Constants;
using Utils.Enums;
using Utils.Extensions;
using Utils.Services;
using AutoMapper;
using Core.Business.Configuracao;

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

        [HttpPost]
        public ActionResult GetQuartos(int EventoId)
        {
            var result = quartosBusiness
                .GetQuartos()
                .Where(x => x.EventoId == EventoId)
                .ToList()
                .Select(x => new QuartoViewModel
                {
                    Id = x.Id,
                    Capacidade = $"{quartosBusiness.GetParticipantesByQuartos(x.Id).Count().ToString()}/{x.Capacidade.ToString()}",
                    Titulo = x.Titulo,
                    Sexo = x.Sexo.GetDescription()
                }).OrderByDescending(x => x.Id);

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
        public ActionResult DistribuirQuartos(int EventoId)
        {
            quartosBusiness.DistribuirQuartos(EventoId);

            return new HttpStatusCodeResult(200);
        }

        [HttpGet]
        public ActionResult GetParticipantesSemQuarto(int EventoId)
        {
            return Json(new { Participantes = quartosBusiness.GetParticipantesSemQuarto(EventoId).Select(x => new { 
                Id = x.Id,
                Nome = x.Nome
            }).ToList() }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetQuartosComParticipantes(int EventoId)
        {
            return Json(new
            {
                Quartos = quartosBusiness.GetQuartosComParticipantes(EventoId).ToList().Select(x => new
                {
                    Nome = UtilServices.CapitalizarNome(x.Participante.Nome),
                    ParticipanteId = x.ParticipanteId,
                    QuartoId = x.QuartoId,
                    Sexo = x.Quarto.Sexo.GetDescription(),
                    Capacidade = $"{quartosBusiness.GetParticipantesByQuartos(x.QuartoId).Count().ToString()}/{x.Quarto.Capacidade.ToString()}",
                }).ToList()
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ChangeQuarto(int ParticipanteId, int? DestinoId)
        {
            var mensagem = quartosBusiness.ChangeQuarto(ParticipanteId, DestinoId);
            if (mensagem == "OK")
            {
                return new HttpStatusCodeResult(200);
            }

            return new HttpStatusCodeResult(400, mensagem);
        }
    }
}