using Arquitetura.Controller;
using Arquitetura.ViewModels;
using Core.Business.Account;
using Core.Business.Arquivos;
using Core.Business.CentroCusto;
using Core.Business.Configuracao;
using Core.Business.ContaBancaria;
using Core.Business.Eventos;
using Core.Business.Lancamento;
using Core.Business.MeioPagamento;
using Core.Business.Participantes;
using Core.Models.Lancamento;
using SysIgreja.ViewModels;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Utils.Constants;
using Utils.Enums;
using Utils.Extensions;
using Utils.Services;

namespace SysIgreja.Controllers
{
    [Authorize(Roles = Usuario.Master + "," + Usuario.Admin + "," + Usuario.Secretaria)]
    public class LancamentoController : SysIgrejaControllerBase
    {
        private readonly IParticipantesBusiness participantesBusiness;
        private readonly IArquivosBusiness arquivosBusiness;
        private readonly ILancamentoBusiness lancamentoBusiness;
        private readonly ICentroCustoBusiness centroCustoBusiness;
        private readonly IMeioPagamentoBusiness meioPagamentoBusiness;
        private readonly IContaBancariaBusiness contaBancariaBusiness;
        private readonly IDatatableService datatableService;

        public LancamentoController(ILancamentoBusiness lancamentoBusiness, IArquivosBusiness arquivosBusiness, ICentroCustoBusiness centroCustoBusiness, IParticipantesBusiness participantesBusiness, IContaBancariaBusiness contaBancariaBusiness, IEventosBusiness eventosBusiness, IAccountBusiness accountBusiness, IConfiguracaoBusiness configuracaoBusiness, IDatatableService datatableService, IMeioPagamentoBusiness meioPagamentoBusiness) : base(eventosBusiness, accountBusiness,configuracaoBusiness)
        {
            this.centroCustoBusiness = centroCustoBusiness;
            this.arquivosBusiness = arquivosBusiness;
            this.participantesBusiness = participantesBusiness;
            this.lancamentoBusiness = lancamentoBusiness;
            this.meioPagamentoBusiness = meioPagamentoBusiness;
            this.contaBancariaBusiness = contaBancariaBusiness;
            this.datatableService = datatableService;
        }

        public ActionResult Index()
        {
            ViewBag.Title = "Financeiro";
            GetEventos();

            ViewBag.MeioPagamentos = meioPagamentoBusiness.GetAllMeioPagamentos().ToList();
            ViewBag.CentroCustoPagar = centroCustoBusiness.GetCentroCustos().Where(x => x.Tipo == TiposCentroCustoEnum.Despesa).ToList();
            ViewBag.CentroCustoReceber = centroCustoBusiness.GetCentroCustos().Where(x => x.Tipo == TiposCentroCustoEnum.Receita).ToList();            
            ViewBag.CentroCustos = centroCustoBusiness.GetCentroCustos().ToList();
            ViewBag.ContasBancarias = contaBancariaBusiness.GetContasBancarias().ToList()
                .Select(x => new ContaBancariaViewModel
                {
                    Banco = x.Banco.GetDescription(),
                    Id = x.Id
                });
            return View();
        }

        [HttpPost]
        public ActionResult GetPagamentos(GetPagamentosModel model)
        {
            var result = (model.ParticipanteId.HasValue ? lancamentoBusiness.GetPagamentosParticipante(model.ParticipanteId.Value) : lancamentoBusiness.GetPagamentosEquipante(model.EquipanteId.Value))
                .ToList()
                .Select(x => MapLancamentoViewModel(x));

            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetLancamentoReceber(FiltrosLancamentoModel model)
        {
            var query = lancamentoBusiness.GetLancamentos().Where(x => x.Tipo == TiposLancamentoEnum.Receber);
            return GetLancamento(model, ref query);
        }

        [HttpPost]
        public ActionResult GetLancamentoPagar(FiltrosLancamentoModel model)
        {
            var query = lancamentoBusiness.GetLancamentos().Where(x => x.Tipo == TiposLancamentoEnum.Pagar);
            return GetLancamento(model, ref query);
        }

        private ActionResult GetLancamento(FiltrosLancamentoModel model, ref IQueryable<Data.Entities.Lancamento> query)
        {
            query = AplicarFiltrosLancamento(model, query);

            var result = query
                .ToList()
                .Select(x => MapLancamentoViewModel(x));

            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }

        private LancamentosViewModel MapLancamentoViewModel(Data.Entities.Lancamento x)
        {
            int qtdAnexos = arquivosBusiness.GetArquivosByLancamento(x.Id).Count();

            return new LancamentosViewModel
            {
                Id = x.Id,
                CentroCusto = x.CentroCusto.Descricao,
                Observacao = x.Observacao,
                Descricao = UtilServices.CapitalizarNome(x.Descricao),
                DataLancamento = x.DataCadastro.Value.ToString("dd/MM/yyyy"),
                FormaPagamento = $"{(x.ContaBancariaId != null ? "Transferência/" + x.ContaBancaria.Banco.GetDescription() : x.MeioPagamento.Descricao)}",
                Valor = UtilServices.DecimalToMoeda(x.Valor),
                ParticipanteId = x.ParticipanteId,
                QtdAnexos = qtdAnexos
            };
        }

        private IQueryable<Data.Entities.Lancamento> AplicarFiltrosLancamento(FiltrosLancamentoModel model, IQueryable<Data.Entities.Lancamento> query)
        {
            if (model.EventoId.HasValue)
            {
                query = query.Where(x => x.EventoId == model.EventoId);
            }

            if (model.MeioPagamentoId.HasValue)
            {
                query = query.Where(x => x.MeioPagamentoId == model.MeioPagamentoId);
            }

            if (model.CentroCustoId.HasValue)
            {
                query = query.Where(x => x.CentroCustoId == model.CentroCustoId);
            }

            if (model.ContaBancariaId.HasValue)
            {
                query = query.Where(x => x.ContaBancariaId == model.ContaBancariaId);
            }

            if (model.DataIni.HasValue && model.DataFim.HasValue)
            {
                query = query.Where(x => x.DataCadastro > model.DataIni.Value && x.DataCadastro < model.DataFim.Value);
            }

            return query;
        }

        [HttpPost]
        public ActionResult DeletePagamento(int Id)
        {
            lancamentoBusiness.DeleteLancamento(Id);

            return new HttpStatusCodeResult(200);
        }

        [HttpPost]
        public ActionResult PostPagamento(PostPagamentoModel model)
        {
            lancamentoBusiness.PostPagamento(model);

            return new HttpStatusCodeResult(200);
        }

        [HttpPost]
        public ActionResult PostLancamento(PostLancamentoModel model)
        {
            lancamentoBusiness.PostLancamento(model);

            return new HttpStatusCodeResult(200);
        }

        [HttpGet]
        public ActionResult GetLancamento(int Id)
        {
            var result = lancamentoBusiness.GetLancamentoById(Id);

            return Json(new { Lancamento = new { 
                result.Id,
                result.CentroCustoId,
                result.Descricao,
                result.Valor,
                result.MeioPagamentoId,
                result.Observacao,
                result.ContaBancariaId

            } 
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetConsolidado(int EventoId)
        {
            var result = lancamentoBusiness
                .GetLancamentos()
                .Where(x => x.EventoId == EventoId)
                .GroupBy(x => new
                {
                    x.Tipo,
                    MeioPagamento = x.MeioPagamento.Descricao.Contains("Cartão") ? "Cartão" : x.MeioPagamento.Descricao
                })                
                .Select(x => new {
                    Tipo = x.Key.Tipo,
                    MeioPagamento = x.Key.MeioPagamento,
                    Valor = x.Sum(y => y.Valor)
                })                
                .ToList()
                .Select(x => new {
                    Tipo = x.Tipo.GetDescription(),
                    MeioPagamento = x.MeioPagamento,
                    Valor = x.Valor
                })
                .OrderByDescending(x => x.Tipo);

            return Json(new { Consolidado = result }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetDetalhado(int EventoId)
        {
            var result = lancamentoBusiness
                .GetLancamentos()
                .Where(x => x.EventoId == EventoId)                
                .ToList()
                .Select(x => new {
                    Tipo = x.Tipo.GetDescription(),
                    MeioPagamento = x.MeioPagamento.Descricao,
                    Valor = x.Valor, 
                    CentroCusto = x.CentroCusto.Descricao,
                    Descricao = UtilServices.CapitalizarNome(x.Descricao),
                    Data = x.DataCadastro.Value.ToString("dd/MM/yyyy")
                })
                .OrderByDescending(x => x.Tipo)
                .OrderBy(x => x.CentroCusto);

            return Json(new { Detalhado = result }, JsonRequestBehavior.AllowGet);
        }
    }
}