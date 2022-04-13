using Arquitetura.Controller;
using Core.Business.Account;
using Core.Business.Configuracao;
using Core.Business.Eventos;
using Core.Business.Reunioes;
using Core.Models.Reunioes;
using SysIgreja.ViewModels;
using System.Linq;
using System.Web.Mvc;
using Utils.Constants;
using Utils.Extensions;

namespace SysIgreja.Controllers
{

    [Authorize(Roles = Usuario.Master + "," + Usuario.Admin + "," + Usuario.Secretaria)]
    public class RelatorioController : SysIgrejaControllerBase
    {
        public RelatorioController(IEventosBusiness eventosBusiness, IAccountBusiness accountBusiness, IConfiguracaoBusiness configuracaoBusiness) :base(eventosBusiness, accountBusiness, configuracaoBusiness)
        {
        }

        public ActionResult Index()
        {
            ViewBag.Title = "Relatórios";
            GetEventos();

            return View();
        }

    }
}