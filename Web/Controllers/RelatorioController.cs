using Arquitetura.Controller;
using Core.Business.Account;
using Core.Business.Configuracao;
using Core.Business.Eventos;
using System.Web.Mvc;
using Utils.Constants;

namespace SysIgreja.Controllers
{

    [Authorize(Roles = Usuario.Master + "," + Usuario.Admin + "," + Usuario.Secretaria)]
    public class RelatorioController : SysIgrejaControllerBase
    {
        public RelatorioController(IEventosBusiness eventosBusiness, IAccountBusiness accountBusiness, IConfiguracaoBusiness configuracaoBusiness) : base(eventosBusiness, accountBusiness, configuracaoBusiness)
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