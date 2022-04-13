using Arquitetura.ViewModels;
using Core.Business.Account;
using Core.Business.Configuracao;
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
        private readonly IConfiguracaoBusiness configuracaoBusiness;

        public SysIgrejaControllerBase(IEventosBusiness eventosBusiness, IAccountBusiness accountBusiness, IConfiguracaoBusiness configuracaoBusiness)
        {
            this.eventosBusiness = eventosBusiness;
            this.accountBusiness = accountBusiness;
            this.configuracaoBusiness = configuracaoBusiness;
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
        }

        public ApplicationUser GetApplicationUser()
        {
            return accountBusiness.GetUsuarioById(User.Identity.GetUserId());
        }
    }
}
