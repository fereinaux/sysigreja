using Core.Business.MeioPagamento;
using Core.Models.MeioPagamento;
using System.Web.Mvc;
using Utils.Constants;

namespace SysIgreja.Controllers
{

    [Authorize(Roles = Usuario.Master + "," + Usuario.Admin)]
    public class MeioPagamentoController : Controller
    {
        private readonly IMeioPagamentoBusiness meioPagamentosBusiness;

        public MeioPagamentoController(IMeioPagamentoBusiness meioPagamentosBusiness)
        {
            this.meioPagamentosBusiness = meioPagamentosBusiness;
        }

        public ActionResult Index()
        {
            ViewBag.Title = "Formas de Pagamento";

            return View();
        }

        [HttpPost]
        public ActionResult GetMeioPagamentos()
        {
            var result = meioPagamentosBusiness.GetMeioPagamentos();

            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetMeioPagamento(int Id)
        {
            var result = meioPagamentosBusiness.GetMeioPagamentoById(Id);

            return Json(new { MeioPagamento = result }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult PostMeioPagamento(PostMeioPagamentoModel model)
        {
            meioPagamentosBusiness.PostMeioPagamento(model);

            return new HttpStatusCodeResult(200);
        }

        [HttpPost]
        public ActionResult DeleteMeioPagamento(int Id)
        {
            meioPagamentosBusiness.DeleteMeioPagamento(Id);

            return new HttpStatusCodeResult(200);
        }
    }
}