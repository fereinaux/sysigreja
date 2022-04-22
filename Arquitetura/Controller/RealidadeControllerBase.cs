using Arquitetura.ViewModels;
using Core.Business.Account;
<<<<<<< HEAD
using Core.Business.Configuracao;
=======
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
using Core.Business.Eventos;
using Data.Context;
using Microsoft.AspNet.Identity;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using Utils.Extensions;

namespace Arquitetura.Controller
{
    [Authorize]
    public class SysIgrejaControllerBase : System.Web.Mvc.Controller
    {
        private readonly IEventosBusiness eventosBusiness;
        private readonly IAccountBusiness accountBusiness;
<<<<<<< HEAD
        private readonly IConfiguracaoBusiness configuracaoBusiness;

        public SysIgrejaControllerBase(IEventosBusiness eventosBusiness, IAccountBusiness accountBusiness, IConfiguracaoBusiness configuracaoBusiness)
        {
            this.eventosBusiness = eventosBusiness;
            this.accountBusiness = accountBusiness;
            this.configuracaoBusiness = configuracaoBusiness;
=======

        public SysIgrejaControllerBase(IEventosBusiness eventosBusiness, IAccountBusiness accountBusiness)
        {
            this.eventosBusiness = eventosBusiness;
            this.accountBusiness = accountBusiness;
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
        }

        [HttpGet]
        public ActionResult DownloadTempFile(string g, string fileName)
        {
            MemoryStream ms = Session[g] as MemoryStream;
            if (ms == null)
            {
                return new EmptyResult();
            }
            Session[g] = null;
            return File(ms, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }

        public void GetEventos()
        {
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
<<<<<<< HEAD
                });
        }

        public void GetConfiguracao()
        {
            ViewBag.Configuracao = configuracaoBusiness
                .GetConfiguracao();
        }

        public void GetCampos()
        {
            ViewBag.Campos = configuracaoBusiness.GetCampos().Select(x => x.Campo).ToList();
=======
                }); 
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
        }

        public ApplicationUser GetApplicationUser()
        {
            return accountBusiness.GetUsuarioById(User.Identity.GetUserId());
        }
    }
}
