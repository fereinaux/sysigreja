using Arquitetura.ViewModels;
using AutoMapper;
using Core.Business.Arquivos;
using Core.Business.Configuracao;
using Core.Business.ContaBancaria;
using Core.Business.Equipantes;
using Core.Business.Equipes;
using Core.Business.Etiquetas;
using Core.Business.Eventos;
using Core.Business.Lancamento;
using Core.Business.MeioPagamento;
using Core.Business.Quartos;
using Core.Business.Reunioes;
using Core.Models.Equipantes;
using Data.Entities;
using SysIgreja.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
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
        private readonly IEtiquetasBusiness etiquetasBusiness;
        private readonly IQuartosBusiness quartosBusiness;
        private readonly IArquivosBusiness arquivoBusiness;
        private readonly IEventosBusiness eventosBusiness;
        private readonly IEquipesBusiness equipesBusiness;
        private readonly IReunioesBusiness reunioesBusiness;
        private readonly ILancamentoBusiness lancamentoBusiness;
        private readonly IMeioPagamentoBusiness meioPagamentoBusiness;
        private readonly IContaBancariaBusiness contaBancariaBusiness;
        private readonly IConfiguracaoBusiness configuracaoBusiness;
        private readonly IDatatableService datatableService;
        private readonly IMapper mapper;
        private readonly int qtdReunioes;


        public EquipanteController(IEquipantesBusiness equipantesBusiness, IEtiquetasBusiness etiquetasBusiness, IConfiguracaoBusiness configuracaoBusiness, IQuartosBusiness quartosBusiness, IDatatableService datatableService, IEventosBusiness eventosBusiness, IEquipesBusiness equipesBusiness, ILancamentoBusiness lancamentoBusiness, IReunioesBusiness reunioesBusiness, IMeioPagamentoBusiness meioPagamentoBusiness, IContaBancariaBusiness contaBancariaBusiness, IArquivosBusiness arquivoBusiness)
        {
            this.quartosBusiness = quartosBusiness;
            this.etiquetasBusiness = etiquetasBusiness;
            this.configuracaoBusiness = configuracaoBusiness;
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
                var eventoId = model.EventoId ?? eventosBusiness.GetEventoAtivo().Id;
                var result = equipantesBusiness.GetEquipantes();

                var totalResultsCount = result.Count();
                var filteredResultsCount = totalResultsCount;


                if (model.Etiquetas != null && model.Etiquetas.Count > 0)
                {
                    model.Etiquetas.ForEach(etiqueta =>
                    result = result.Where(x => x.ParticipantesEtiquetas.Any(y => y.EtiquetaId.ToString() == etiqueta)));

                }

                if (model.NaoEtiquetas != null && model.NaoEtiquetas.Count > 0)
                {
                    model.NaoEtiquetas.ForEach(etiqueta =>
                 result = result.Where(x => !x.ParticipantesEtiquetas.Any(y => y.EtiquetaId.ToString() == etiqueta)));
                }

                if (model.EventoId != null)
                {

                    result = result.Where(x => x.Equipes.Any(y => y.EventoId == model.EventoId));
                    
                    filteredResultsCount = result.Count();
                }

                if (model.Status != null)
                {
                    if (model.Status == "pendente")
                    {
                        result = result.Where(x => (!x.Lancamentos.Any(y => y.EventoId ==  (x.Equipes.Any() ? x.Equipes.OrderByDescending(z => z.EventoId).FirstOrDefault().EventoId : 0))));
                    }
                    else if (model.Status == "pago")
                    {
                        result = result.Where(x => (x.Lancamentos.Any(y => y.EventoId == (x.Equipes.Any() ? x.Equipes.OrderByDescending(z => z.EventoId).FirstOrDefault().EventoId : 0))));

                    }
                    filteredResultsCount = result.Count();
                }

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
            result.Equipe = equipeAtual?.Equipe.GetDescription() ?? "";
            result.Checkin = equipeAtual?.Checkin ?? false;

            var etiquetas = etiquetasBusiness.GetEtiquetas().ToList()
              .Select(x => new
              {
                  Nome = x.Nome,
                  Id = x.Id,
                  Cor = x.Cor
              });

            var equipante = mapper.Map<PostEquipanteModel>(result);

            var dadosAdicionais = new
            {
                Status = result.Status.GetDescription(),
                Quarto = quartosBusiness.GetQuartosComParticipantes(eventoId, TipoPessoaEnum.Equipante).Where(x => x.EquipanteId == Id).FirstOrDefault()?.Quarto?.Titulo ?? ""
            };

            return Json(new { Equipante = equipante, Etiquetas = etiquetas, }, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult VerificaCadastro(string Fone)
        {
            var equipante = equipantesBusiness.GetEquipantes().FirstOrDefault(x => x.Fone.Replace("+", "").Replace("(", "").Replace(")", "").Replace(".", "").Replace("-", "") == Fone.Replace("+", "").Replace("(", "").Replace(")", "").Replace(".", "").Replace("-", ""));

            if (equipante != null)
                return Json(Url.Action("InscricaoConcluida", new { Id = equipante.Id }));
            else
                return new HttpStatusCodeResult(200);
        }

        [AllowAnonymous]
        public ActionResult InscricaoConcluida(int Id)
        {
            Equipante equipante = equipantesBusiness.GetEquipanteById(Id);
            var eventoAtual = eventosBusiness.GetEventoAtivo();
            ViewBag.Configuracao = configuracaoBusiness.GetConfiguracao();
            ViewBag.Participante = new InscricaoConcluidaViewModel
            {
                Id = equipante.Id,
                Apelido = equipante.Nome,
                Logo = eventoAtual.TipoEvento.GetNickname() + ".png",
                Evento = $"{eventoAtual.TipoEvento.GetDescription()}",
                Valor = eventoAtual.Valor.ToString("C", CultureInfo.CreateSpecificCulture("pt-BR")),
                DataEvento = eventoAtual.DataEvento.ToString("dd/MM/yyyy"),
            };

            return View("InscricaoConcluida");

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

            var etiquetas = etiquetasBusiness.GetEtiquetas().ToList()
           .Select(x => new
           {
               Nome = x.Nome,
               Id = x.Id,
               Cor = x.Cor
           });

            return Json(new { Equipante = equipante, Etiquetas = etiquetas }, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult PostEquipante(PostEquipanteModel model)
        {
            var equipante = equipantesBusiness.PostEquipante(model);

            if (model.Inscricao)
            {
                return Json(Url.Action("InscricaoConcluida", new { Id = equipante.Id }));
            }
            else
            {
                return new HttpStatusCodeResult(200);
            }

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