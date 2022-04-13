using Core.Business.CentroCusto;
using Core.Business.Mensagem;
using Core.Models.CentroCusto;
using Core.Models.Mensagem;
using SysIgreja.ViewModels;
using System.Linq;
using System.Web.Mvc;
using Utils.Constants;
using Utils.Enums;
using static Utils.Extensions.EnumExtensions;

namespace SysIgreja.Controllers
{

    [Authorize(Roles = Usuario.Master + "," + Usuario.Admin + "," + Usuario.Secretaria)]
    public class MensagemController : Controller
    {
        private readonly IMensagemBusiness mensagemBusiness;

        public MensagemController(IMensagemBusiness mensagemBusiness)
        {
            this.mensagemBusiness = mensagemBusiness;
        }

        public ActionResult Index()
        {
            ViewBag.Title = "Mensagens";
            return View();
        }

        [HttpPost]
        public ActionResult GetMensagens()
        {
            var result = mensagemBusiness
                .GetMensagems()
                .ToList()
                .Select(x => new PostMessageModel
                {
                    Titulo = x.Titulo,
                    Conteudo =x.Conteudo,
                    Id = x.Id
                });

            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetMensagem(int Id)
        {
            var result = mensagemBusiness.GetMensagemById(Id);

            return Json(new { Mensagem = result }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult PostMensagem(PostMessageModel model)
        {
            mensagemBusiness.PostMensagem(model);

            return new HttpStatusCodeResult(200);
        }

        [HttpPost]
        public ActionResult DeleteMensasgem(int Id)
        {
            mensagemBusiness.DeleteMensagem(Id);

            return new HttpStatusCodeResult(200);
        }
    }
}