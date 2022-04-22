﻿using Arquitetura.Controller;
<<<<<<< HEAD
using Core.Business.Account;
using Core.Business.Configuracao;
=======
using Arquitetura.ViewModels;
using Core.Business.Account;
using Core.Business.Equipantes;
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
using Core.Business.Equipes;
using Core.Business.Eventos;
using Core.Business.Reunioes;
using Core.Models.Equipe;
using SysIgreja.ViewModels;
using System;
<<<<<<< HEAD
=======
using System.Collections.Generic;
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
using System.Linq;
using System.Web.Mvc;
using Utils.Enums;
using Utils.Extensions;
using Utils.Services;

namespace SysIgreja.Controllers
{

    [Authorize]
    public class EquipeController : SysIgrejaControllerBase
    {
        private readonly IEquipesBusiness equipesBusiness;
        private readonly IReunioesBusiness reunioesBusiness;
<<<<<<< HEAD
        private readonly IEventosBusiness eventosBusiness;

        public EquipeController(IEquipesBusiness equipesBusiness, IConfiguracaoBusiness configuracaoBusiness, IEventosBusiness eventosBusiness, IAccountBusiness accountBusiness, IReunioesBusiness reunioesBusiness) : base(eventosBusiness, accountBusiness, configuracaoBusiness)
        {
            this.equipesBusiness = equipesBusiness;
            this.reunioesBusiness = reunioesBusiness;
            this.eventosBusiness = eventosBusiness;
=======

        public EquipeController(IEquipesBusiness equipesBusiness, IEventosBusiness eventosBusiness, IAccountBusiness accountBusiness, IReunioesBusiness reunioesBusiness) : base(eventosBusiness, accountBusiness)
        {
            this.equipesBusiness = equipesBusiness;
            this.reunioesBusiness = reunioesBusiness;
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
        }

        public ActionResult Index()
        {
            ViewBag.Title = "Equipes";
            GetEventos();

            return View();
        }

        public ActionResult Presenca()
        {
            ViewBag.Title = "Ata de Presença";
            GetEventos();
            return View();
        }

        [HttpGet]
        public ActionResult GetEquipesByUser(int EventoId)
        {
            var user = GetApplicationUser();

            if (user.Perfil == PerfisUsuarioEnum.Coordenador)
            {
                return Json(new
                {
                    Equipes = equipesBusiness.GetEquipes()
                    .Where(x =>
                    x.Description == equipesBusiness.GetEquipanteEventoByUser(EventoId, user.Id)
                        .Equipe.GetDescription())
                        .ToList()
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Equipes = equipesBusiness.GetEquipes(EventoId).ToList() }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult TogglePresenca(int EquipanteEventoId, int ReuniaoId)
        {
            equipesBusiness.TogglePresenca(EquipanteEventoId, ReuniaoId);

            return new HttpStatusCodeResult(200);
        }

        [HttpGet]
        public ActionResult GetReunioes(int EventoId)
        {
            var result = reunioesBusiness.GetReunioes(EventoId)
                .ToList()
                .OrderBy(x => DateTime.Now.AddHours(4).Subtract(x.DataReuniao).TotalDays < 0 ? DateTime.Now.AddHours(4).Subtract(x.DataReuniao).TotalDays * -1 : DateTime.Now.AddHours(4).Subtract(x.DataReuniao).TotalDays)
                .Select(x => new ReuniaoViewModel { DataReuniao = x.DataReuniao, Id = x.Id });

            return Json(new { Reunioes = result }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
<<<<<<< HEAD
        public ActionResult GetEquipes(int? EventoId)
        {
            EventoId = EventoId ?? eventosBusiness.GetEventoAtivo().Id;
=======
        public ActionResult GetEquipes(int EventoId)
        {
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
            var result = equipesBusiness.GetEquipes(EventoId).Select(x => new ListaEquipesViewModel
            {
                Id = x.Id,
                Equipe = x.Description,
<<<<<<< HEAD
                QuantidadeMembros = equipesBusiness.GetMembrosEquipe(EventoId.Value, (EquipesEnum)x.Id).Count()
=======
                QuantidadeMembros = equipesBusiness.GetMembrosEquipe(EventoId, (EquipesEnum)x.Id).Count()
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
            });

            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetPresenca(int EventoId, EquipesEnum EquipeId, int ReuniaoId)
        {
            var presenca = equipesBusiness.GetPresenca(ReuniaoId).Select(x => x.EquipanteEventoId).ToList();

            var result = equipesBusiness
                .GetMembrosEquipe(EventoId, EquipeId).ToList().Select(x => new PresencaViewModel
                {
                    Id = x.Id,
                    Nome = x.Equipante.Nome,
                    Presenca = presenca.Contains(x.Id)
                });

            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetEquipantes(int EventoId)
        {
            var result = equipesBusiness.GetEquipantesEventoSemEquipe(EventoId).Select(x => new { x.Id, x.Nome });

            return Json(new { Equipantes = result }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetEquipantesByEventoSelect(int EventoId)
        {
            var result = equipesBusiness.GetEquipantesByEvento(EventoId)
                .OrderBy(x => x.Nome)
                .Select(x => new
                {
                    Id = x.Id,
                    Nome = UtilServices.CapitalizarNome(x.Nome),
                    Apelido = UtilServices.CapitalizarNome(x.Apelido),
                });

            var json = Json(new { data = result }, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = Int32.MaxValue;
            return json;
        }

        [HttpGet]
        public ActionResult GetEquipantesByEvento(int EventoId)
        {
            var result = equipesBusiness.GetEquipantesByEvento(EventoId)
                .Select(x => new
                {
                    Id = x.Id,
                    Sexo = x.Sexo.GetDescription(),
                    Fone = x.Fone,
                    Idade = UtilServices.GetAge(x.DataNascimento),
                    Equipe = equipesBusiness.GetEquipeAtual(EventoId, x.Id)?.Equipe.GetDescription() ?? "",
                    Nome = UtilServices.CapitalizarNome(x.Nome),
                    Apelido = UtilServices.CapitalizarNome(x.Apelido),
                    Foto = x.Arquivos.Any(y => y.IsFoto) ? Convert.ToBase64String(x.Arquivos.FirstOrDefault(y => y.IsFoto).Conteudo) : ""
<<<<<<< HEAD
                }).ToList().OrderBy(x => x.Equipe).ThenBy(x => x.Nome);
=======
                }).ToList().OrderBy(x => x.Equipe ).ThenBy(x => x.Nome);
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566

            var json = Json(new { data = result }, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = Int32.MaxValue;
            return json;
        }

        [HttpPost]
<<<<<<< HEAD
        public ActionResult GetMembrosEquipe(int EventoId, EquipesEnum EquipeId, bool Foto)
=======
        public ActionResult GetMembrosEquipe(int EventoId, EquipesEnum EquipeId)
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
        {
            var query = equipesBusiness
                .GetMembrosEquipe(EventoId, EquipeId)
                .ToList();

            var result = query
            .Select(x => new
            {
                Id = x.Id,
                Nome = x.Equipante.Nome,
                Apelido = x.Equipante.Apelido,
                Equipe = x.Equipe.GetDescription(),
                Idade = UtilServices.GetAge(x.Equipante.DataNascimento),
                Tipo = x.Tipo.GetDescription(),
                x.Equipante.Fone,
<<<<<<< HEAD
                Foto = Foto && x.Equipante.Arquivos.Any(y => y.IsFoto) ? Convert.ToBase64String(x.Equipante.Arquivos.FirstOrDefault(y => y.IsFoto).Conteudo) : ""
=======
                Foto = x.Equipante.Arquivos.Any(y => y.IsFoto) ? Convert.ToBase64String(x.Equipante.Arquivos.FirstOrDefault(y => y.IsFoto).Conteudo) : ""
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
            });

            var json = Json(new { data = result }, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = Int32.MaxValue;
            return json;
        }

        [HttpPost]
        public ActionResult ToggleMembroEquipeTipo(int Id)
        {
            equipesBusiness.ToggleMembroEquipeTipo(Id);
            return new HttpStatusCodeResult(200);
        }

        [HttpPost]
        public ActionResult AddMembroEquipe(PostEquipeMembroModel model)
        {
            equipesBusiness.AddMembroEquipe(model);
            return new HttpStatusCodeResult(200);
        }

        [HttpPost]
        public ActionResult DeleteMembroEquipe(int Id)
        {
            equipesBusiness.DeleteMembroEquipe(Id);
            return new HttpStatusCodeResult(200);
        }
    }
}