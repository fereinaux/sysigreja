using Arquitetura.Controller;
using Core.Business.Account;
<<<<<<< HEAD
using Core.Business.Configuracao;
using Core.Business.Eventos;
using System.Web.Mvc;
using Utils.Constants;
=======
using Core.Business.Eventos;
using Core.Business.Reunioes;
using Core.Models.Reunioes;
using SysIgreja.ViewModels;
using System.Linq;
using System.Web.Mvc;
using Utils.Constants;
using Utils.Extensions;
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566

namespace SysIgreja.Controllers
{

    [Authorize(Roles = Usuario.Master + "," + Usuario.Admin + "," + Usuario.Secretaria)]
    public class RelatorioController : SysIgrejaControllerBase
    {
<<<<<<< HEAD
        public RelatorioController(IEventosBusiness eventosBusiness, IAccountBusiness accountBusiness, IConfiguracaoBusiness configuracaoBusiness) : base(eventosBusiness, accountBusiness, configuracaoBusiness)
=======
        public RelatorioController(IEventosBusiness eventosBusiness, IAccountBusiness accountBusiness) :base(eventosBusiness, accountBusiness)
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
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