using Arquitetura.ViewModels;
using AutoMapper;
using Core.Business.Arquivos;
using Core.Business.ContaBancaria;
using Core.Business.Equipantes;
using Core.Business.Equipes;
using Core.Business.Eventos;
using Core.Business.Lancamento;
using Core.Business.MeioPagamento;
using Core.Business.Reunioes;
using Core.Models.Equipantes;
using Core.Models.Lancamento;
using Data.Entities;
using SysIgreja.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Web.Mvc;
using Utils.Constants;
using Utils.Enums;
using Utils.Extensions;
using Utils.Services;

namespace SysIgreja.Controllers
{

    [Authorize(Roles = Usuario.Master + "," + Usuario.Admin + "," + Usuario.Secretaria)]
    public class EquipanteController : Controller
    {
        private readonly IEquipantesBusiness equipantesBusiness;
        private readonly IArquivosBusiness arquivoBusiness;
        private readonly IEventosBusiness eventosBusiness;
        private readonly IEquipesBusiness equipesBusiness;
        private readonly IReunioesBusiness reunioesBusiness;
        private readonly ILancamentoBusiness lancamentoBusiness;
        private readonly IMeioPagamentoBusiness meioPagamentoBusiness;
        private readonly IContaBancariaBusiness contaBancariaBusiness;
        private readonly IMapper mapper;


        public EquipanteController(IEquipantesBusiness equipantesBusiness, IEventosBusiness eventosBusiness, IEquipesBusiness equipesBusiness, ILancamentoBusiness lancamentoBusiness, IReunioesBusiness reunioesBusiness, IMeioPagamentoBusiness meioPagamentoBusiness, IContaBancariaBusiness contaBancariaBusiness, IArquivosBusiness arquivoBusiness)
        {
            this.equipantesBusiness = equipantesBusiness;
            this.eventosBusiness = eventosBusiness;
            this.equipesBusiness = equipesBusiness;
            this.arquivoBusiness = arquivoBusiness;
            this.lancamentoBusiness = lancamentoBusiness;
            this.contaBancariaBusiness = contaBancariaBusiness;
            this.meioPagamentoBusiness = meioPagamentoBusiness;
            this.reunioesBusiness = reunioesBusiness;
            var eventoAtivo = eventosBusiness.GetEventoAtivo() ?? eventosBusiness.GetEventos().ToList().LastOrDefault();
            mapper = new MapperRealidade(reunioesBusiness.GetReunioes(eventoAtivo.Id).Where(x => x.DataReuniao < System.DateTime.Today).Count()).mapper;
        }



        public ActionResult Index()
        {
            ViewBag.Title = "Equipantes";
            ViewBag.Eventos = eventosBusiness
               .GetEventos()
               .OrderByDescending(x => x.DataEvento)
               .ToList()
               .Select(x => new EventoViewModel
               {
                   Id = x.Id,
                   DataEvento = x.DataEvento,
                   Numeracao = x.Numeracao,
                   TipoEvento = x.TipoEvento.GetNickname(),
                   Status = x.Status.GetDescription()
               });
            ViewBag.MeioPagamentos = meioPagamentoBusiness.GetAllMeioPagamentos().ToList();
            ViewBag.Valor = (int)ValoresPadraoEnum.TaxaEquipante;
            ViewBag.ContasBancarias = contaBancariaBusiness.GetContasBancarias().ToList()
                .Select(x => new ContaBancariaViewModel
                {
                    Banco = x.Banco.GetDescription(),
                    Id = x.Id
                });

            return View();
        }

        [HttpPost]
        public ActionResult GetEquipantesDataTable(FilterModel model)
        {

            var result = equipantesBusiness.GetEquipantes();

            var totalResultsCount = result.Count();
            var filteredResultsCount = totalResultsCount;

            if (model.search.value != null)
            {
                result = result.Where(x => (x.Nome.Contains(model.search.value)));
                filteredResultsCount = result.Count();
            }

            try
            {
                result = result.OrderBy(model.columns[model.order[0].column].name + " " + model.order[0].dir);
            }
            catch (Exception)
            {
            }

            result = result.Skip(model.Start)
            .Take(model.Length);

            return Json(new
            {
                data = mapper.Map<IEnumerable<EquipanteListModel>>(result),
                recordsTotal = totalResultsCount,
                recordsFiltered = filteredResultsCount,
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetEquipantes()
        {

            var result = equipantesBusiness.GetEquipantes();
            
            return Json(new { data = mapper.Map<IEnumerable<EquipanteListModel>>(result) }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetEquipante(int Id)
        {
            var result = equipantesBusiness.GetEquipanteById(Id);
            int eventoId = (eventosBusiness.GetEventoAtivo() ?? eventosBusiness.GetEventos().OrderByDescending(x => x.DataEvento).First()).Id;

            result.Nome = UtilServices.CapitalizarNome(result.Nome);
            result.Apelido = UtilServices.CapitalizarNome(result.Apelido);
            result.Equipe = equipesBusiness.GetEquipeAtual(eventoId, result.Id)?.Equipe.GetDescription() ?? "";

            var equipante = mapper.Map<PostEquipanteModel>(result);

            return Json(new { Equipante = equipante }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult PostEquipante(PostEquipanteModel model)
        {
            equipantesBusiness.PostEquipante(model);

            return new HttpStatusCodeResult(200);
        }

        [HttpPost]
        public ActionResult DeleteEquipante(int Id)
        {
            equipantesBusiness.DeleteEquipante(Id);

            return new HttpStatusCodeResult(200);
        }

        [HttpPost]
        public ActionResult ToggleSexo(int Id)
        {
            equipantesBusiness.ToggleSexo(Id);

            return new HttpStatusCodeResult(200);
        }

        [HttpPost]
        public ActionResult ToggleVacina(int Id)
        {
            equipantesBusiness.ToggleVacina(Id);

            return new HttpStatusCodeResult(200);
        }

        [HttpPost]
        public ActionResult ToggleTeste(int Id)
        {
            equipantesBusiness.ToggleTeste(Id);

            return new HttpStatusCodeResult(200);
        }

        [HttpPost]
        public ActionResult ToggleCheckin(int Id)
        {
            equipantesBusiness.ToggleCheckin(Id);

            return new HttpStatusCodeResult(200);
        }
    }
}