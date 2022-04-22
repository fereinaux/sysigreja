using Arquitetura.Controller;
<<<<<<< HEAD
using AutoMapper;
using Core.Business.Account;
using Core.Business.Configuracao;
using Core.Business.Equipes;
using Core.Business.Eventos;
using Core.Business.Quartos;
using Core.Models.Quartos;
=======
using Core.Business.Account;
using Core.Business.Quartos;
using Core.Business.Equipes;
using Core.Business.Eventos;
using Core.Business.Reunioes;
using Core.Models.Quartos;
using Core.Models.Reunioes;
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
using SysIgreja.ViewModels;
using System.Linq;
using System.Web.Mvc;
using Utils.Constants;
using Utils.Enums;
using Utils.Extensions;
using Utils.Services;
<<<<<<< HEAD
=======
using AutoMapper;
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566

namespace SysIgreja.Controllers
{

    [Authorize(Roles = Usuario.Master + "," + Usuario.Admin + "," + Usuario.Secretaria)]
    public class QuartoController : SysIgrejaControllerBase
    {
        private readonly IQuartosBusiness quartosBusiness;
        private readonly IEquipesBusiness equipesBusiness;
        private readonly IMapper mapper;

<<<<<<< HEAD
        public QuartoController(IQuartosBusiness quartosBusiness, IEquipesBusiness equipesBusiness, IEventosBusiness eventosBusiness, IAccountBusiness accountBusiness, IConfiguracaoBusiness configuracaoBusiness) : base(eventosBusiness, accountBusiness, configuracaoBusiness)
=======
        public QuartoController(IQuartosBusiness quartosBusiness, IEquipesBusiness equipesBusiness, IEventosBusiness eventosBusiness, IAccountBusiness accountBusiness) : base(eventosBusiness, accountBusiness)
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
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

<<<<<<< HEAD
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
=======
        [HttpPost]
        public ActionResult GetQuartos(int EventoId)
        {
            var result = quartosBusiness
                .GetQuartos()
                .Where(x => x.EventoId == EventoId)
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
                .ToList()
                .Select(x => new QuartoViewModel
                {
                    Id = x.Id,
<<<<<<< HEAD
                    Capacidade = $"{quartosBusiness.GetParticipantesByQuartos(x.Id, tipo).Count().ToString()}/{x.Capacidade.ToString()}",
=======
                    Capacidade = $"{quartosBusiness.GetParticipantesByQuartos(x.Id).Count().ToString()}/{x.Capacidade.ToString()}",
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
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
<<<<<<< HEAD
        public ActionResult DistribuirQuartos(int EventoId, TipoPessoaEnum? tipo)
        {
            quartosBusiness.DistribuirQuartos(EventoId, tipo ?? TipoPessoaEnum.Participante);
=======
        public ActionResult DistribuirQuartos(int EventoId)
        {
            quartosBusiness.DistribuirQuartos(EventoId);
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566

            return new HttpStatusCodeResult(200);
        }

        [HttpGet]
<<<<<<< HEAD
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
=======
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
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
            if (mensagem == "OK")
            {
                return new HttpStatusCodeResult(200);
            }

            return new HttpStatusCodeResult(400, mensagem);
        }
<<<<<<< HEAD

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
=======
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
    }
}