﻿using Arquitetura.Controller;
using Core.Business.Account;
using Core.Business.Circulos;
<<<<<<< HEAD
using Core.Business.Configuracao;
using Core.Business.Equipes;
using Core.Business.Eventos;
using Core.Models.Circulos;
=======
using Core.Business.Equipes;
using Core.Business.Eventos;
using Core.Business.Reunioes;
using Core.Models.Circulos;
using Core.Models.Reunioes;
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
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
    public class CirculoController : SysIgrejaControllerBase
    {
        private readonly ICirculosBusiness circulosBusiness;
        private readonly IEquipesBusiness equipesBusiness;

<<<<<<< HEAD
        public CirculoController(ICirculosBusiness circulosBusiness, IEquipesBusiness equipesBusiness, IEventosBusiness eventosBusiness, IAccountBusiness accountBusiness, IConfiguracaoBusiness configuracaoBusiness) : base(eventosBusiness, accountBusiness, configuracaoBusiness)
=======
        public CirculoController(ICirculosBusiness circulosBusiness, IEquipesBusiness equipesBusiness,IEventosBusiness eventosBusiness, IAccountBusiness accountBusiness) :base(eventosBusiness, accountBusiness)
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
        {
            this.circulosBusiness = circulosBusiness;
            this.equipesBusiness = equipesBusiness;
        }

        public ActionResult Index()
        {
            ViewBag.Title = "Círculos";
            GetEventos();
<<<<<<< HEAD
            GetConfiguracao();
=======
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566

            return View();
        }

        [HttpPost]
        public ActionResult GetCirculos(int EventoId)
        {
            var result = circulosBusiness
                .GetCirculos()
                .Where(x => x.EventoId == EventoId)
                .ToList()
                .Select(x => new CirculoViewModel
                {
                    Id = x.Id,
                    Dirigente1 = x.Dirigente1 != null ? UtilServices.CapitalizarNome(x.Dirigente1.Equipante.Nome) : "",
                    Dirigente2 = x.Dirigente2 != null ? UtilServices.CapitalizarNome(x.Dirigente2.Equipante.Nome) : "",
                    QtdParticipantes = circulosBusiness.GetParticipantesByCirculos(x.Id).Count(),
                    Cor = x.Cor.GetDescription()
                });

            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetCirculo(int Id)
        {
            var result = circulosBusiness.GetCirculoById(Id);

            return Json(new { Circulo = result }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult PostCirculo(PostCirculoModel model)
        {
            circulosBusiness.PostCirculo(model);

            return new HttpStatusCodeResult(200);
        }

        [HttpPost]
        public ActionResult DeleteCirculo(int Id)
        {
            circulosBusiness.DeleteCirculo(Id);

            return new HttpStatusCodeResult(200);
        }

        [HttpPost]
        public ActionResult DistribuirCirculos(int EventoId)
        {
            circulosBusiness.DistribuirCirculos(EventoId);

            return new HttpStatusCodeResult(200);
        }

        [HttpGet]
        public ActionResult GetEquipantes(int EventoId)
        {
            var circuloList = circulosBusiness.GetCirculos().Where(x => x.EventoId == EventoId).Select(x => x.Dirigente1Id);
<<<<<<< HEAD
            var pgList = equipesBusiness.GetMembrosEquipe(EventoId, EquipesEnum.Circulo).ToList().Where(x => !circuloList.Contains(x.Id)).Select(x => new { x.Id, Nome = x.Equipante.Nome }).ToList();
=======
            var pgList = equipesBusiness.GetMembrosEquipe(EventoId, EquipesEnum.Circulo).ToList().Where(x => !circuloList.Contains(x.Id)).Select(x => new { x.Id, Nome = x.Equipante.Nome}).ToList();
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566

            return Json(new { Equipantes = pgList }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetParticipantesSemCirculo(int EventoId)
        {
<<<<<<< HEAD
            return Json(new { Participantes = circulosBusiness.GetParticipantesSemCirculo(EventoId).Select(x => new { x.Id, x.Nome }).ToList() }, JsonRequestBehavior.AllowGet);
=======
            return Json(new { Participantes = circulosBusiness.GetParticipantesSemCirculo(EventoId).Select(x => new {x.Id, x.Nome }).ToList() }, JsonRequestBehavior.AllowGet);
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
        }

        [HttpGet]
        public ActionResult GetCirculosComParticipantes(int EventoId)
        {
            return Json(new
            {
<<<<<<< HEAD
                Circulos = circulosBusiness.GetCirculosComParticipantes(EventoId).ToList().Select(x => new
                {
                    Nome = UtilServices.CapitalizarNome(x.Participante.Nome),
                    Latitude = x.Participante.Latitude,
                    Longitude = x.Participante.Longitude,
                    ParticipanteId = x.ParticipanteId,
                    CEP = x.Participante.CEP,
=======
                Circulos = circulosBusiness.GetCirculosComParticipantes(EventoId).ToList().Select(x => new {
                    Nome = UtilServices.CapitalizarNome(x.Participante.Nome),
                    ParticipanteId = x.ParticipanteId,
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
                    CirculoId = x.CirculoId,
                    Cor = x.Circulo.Cor.GetDescription(),
                    Equipante = x.Circulo.Dirigente1 != null ? UtilServices.CapitalizarNome(x.Circulo.Dirigente1.Equipante.Nome) : ""
                }).ToList()
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ChangeCirculo(int ParticipanteId, int? DestinoId)
        {
            circulosBusiness.ChangeCirculo(ParticipanteId, DestinoId);

            return new HttpStatusCodeResult(200);
        }

        [HttpGet]
<<<<<<< HEAD
        public ActionResult GetCoresAtivas(int EventoId)
        {

            var coresList = circulosBusiness.GetCirculos().Where(x => x.EventoId == EventoId).ToList().Select(x => new { Cor = x.Cor.GetDescription(), Id = x.Id });
            return Json(new
            {
                Cores = coresList
            }, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
=======
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
        public ActionResult GetCores(int EventoId)
        {
            var circuloList = circulosBusiness.GetCirculos().Where(x => x.EventoId == EventoId).ToList().Select(x => x.Cor.GetDescription());

            var coresList = circulosBusiness.GetCores(EventoId).ToList().Where(x => !circuloList.Contains(x.Description));

            return Json(new { Cores = coresList }, JsonRequestBehavior.AllowGet);
        }
    }
}