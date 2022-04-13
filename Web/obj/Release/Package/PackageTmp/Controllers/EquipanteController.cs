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
        private readonly IDatatableService datatableService;
        private readonly IMapper mapper;
        private readonly int qtdReunioes;


        public EquipanteController(IEquipantesBusiness equipantesBusiness, IDatatableService datatableService, IEventosBusiness eventosBusiness, IEquipesBusiness equipesBusiness, ILancamentoBusiness lancamentoBusiness, IReunioesBusiness reunioesBusiness, IMeioPagamentoBusiness meioPagamentoBusiness, IContaBancariaBusiness contaBancariaBusiness, IArquivosBusiness arquivoBusiness)
        {
            this.equipantesBusiness = equipantesBusiness;
            this.eventosBusiness = eventosBusiness;
            this.equipesBusiness = equipesBusiness;
            this.arquivoBusiness = arquivoBusiness;
            this.lancamentoBusiness = lancamentoBusiness;
            this.contaBancariaBusiness = contaBancariaBusiness;
            this.meioPagamentoBusiness = meioPagamentoBusiness;
            this.reunioesBusiness = reunioesBusiness;
            this.datatableService = datatableService;
            var eventoAtivo = eventosBusiness.GetEventoAtivo() ?? eventosBusiness.GetEventos().ToList().LastOrDefault();
            qtdReunioes = reunioesBusiness.GetReunioes(eventosBusiness.GetEventoAtivo().Id).Where(x => x.DataReuniao < System.DateTime.Today).Count();
            mapper = new MapperRealidade(qtdReunioes, eventoAtivo.Id).mapper;
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
        public ActionResult getEquipantesExcel(int eventoid)
        {
            Guid g = Guid.NewGuid();

            Session[g.ToString()] = datatableService.GenerateExcel(mapper.Map<IEnumerable<EquipanteExcelModel>>(equipesBusiness.GetEquipantesByEvento(eventoid).OrderBy(x => x.Nome)));

            return Content(g.ToString());
        }


        [HttpPost]
        public ActionResult GetEquipantesDataTable(FilterModel model)
        {

            var extract = Request.QueryString["extract"];
            if (extract == "excel")
            {
                Guid g = Guid.NewGuid();

                Session[g.ToString()] = datatableService.GenerateExcel(mapper.Map<IEnumerable<EquipanteExcelModel>>(equipantesBusiness.GetEquipantes()));

                return Content(g.ToString());
            }
            else
            {
                var eventoId = eventosBusiness.GetEventoAtivo().Id;
                var result = equipantesBusiness.GetEquipantes();

                var totalResultsCount = result.Count();
                var filteredResultsCount = totalResultsCount;

                if (model.Equipe != null)
                {
                    result = result.Where(x => x.Equipes.Any() && x.Equipes.OrderByDescending(z => z.EventoId).FirstOrDefault(y => y.EventoId == eventoId).Equipe == model.Equipe);
                    filteredResultsCount = result.Count();
                }

                if (model.search.value != null)
                {
                    result = result.Where(x => (x.Nome.Contains(model.search.value)));
                    filteredResultsCount = result.Count();
                }

                try
                {
                    if (model.columns[model.order[0].column].name == "HasOferta")
                    {
                        if (model.order[0].dir == "asc")
                        {
                            result = result.OrderBy(x => new
                            {
                                Order = x.Lancamentos.Where(y => y.EventoId == eventoId).Any()
                            });

                        }
                        else
                        {
                            result = result.OrderByDescending(x => new
                            {
                                Order = x.Lancamentos.Where(y => y.EventoId == eventoId).Any()
                            });
                        }

                    }
                    else if (model.columns[model.order[0].column].name == "Faltas")
                    {
                        if (model.order[0].dir == "asc")
                        {
                            result = result.OrderBy(x => new
                            {
                                Order = qtdReunioes - x.Equipes.OrderByDescending(z => z.EventoId).FirstOrDefault().Presencas.Count()
                            });

                        }
                        else
                        {
                            result = result.OrderByDescending(x => new
                            {
                                Order = qtdReunioes - x.Equipes.OrderByDescending(z => z.EventoId).FirstOrDefault().Presencas.Count()
                            });
                        }

                    }
                    else if (model.columns[model.order[0].column].name == "Equipe")
                    {
                        if (model.order[0].dir == "asc")
                        {
                            result = result.OrderBy(x => new
                            {
                                Order = x.Equipes.OrderByDescending(z => z.EventoId).FirstOrDefault().Equipe
                            });

                        }
                        else
                        {
                            result = result.OrderByDescending(x => new
                            {
                                Order = x.Equipes.OrderByDescending(z => z.EventoId).FirstOrDefault().Equipe
                            });
                        }

                    }
                    else
                    {
                        result = result.OrderBy(model.columns[model.order[0].column].name + " " + model.order[0].dir);
                    }
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
            var equipeAtual = equipesBusiness.GetEquipeAtual(eventoId, result.Id);
            result.Equipe = equipeAtual.Equipe.GetDescription() ?? "";
            result.Checkin = equipeAtual.Checkin;

            var equipante = mapper.Map<PostEquipanteModel>(result);

            return Json(new { Equipante = equipante }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetEquipanteEvento(int Id, int eventoId)
        {
            var result = equipantesBusiness.GetEquipanteById(Id);

            result.Nome = UtilServices.CapitalizarNome(result.Nome);
            result.Apelido = UtilServices.CapitalizarNome(result.Apelido);
            var equipeAtual = equipesBusiness.GetEquipeAtual(eventoId, result.Id);
            result.Equipe = equipeAtual.Equipe.GetDescription() ?? "";
            result.Checkin = equipeAtual.Checkin;

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
        public ActionResult ToggleCheckin(int Id, int eventoid)
        {
            equipantesBusiness.ToggleCheckin(Id, eventoid);

            return new HttpStatusCodeResult(200);
        }
    }
}