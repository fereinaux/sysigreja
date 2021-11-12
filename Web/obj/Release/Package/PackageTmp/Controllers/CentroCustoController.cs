using Core.Business.CentroCusto;
using Core.Models.CentroCusto;
using SysIgreja.ViewModels;
using System.Linq;
using System.Web.Mvc;
using Utils.Constants;
using Utils.Enums;
using static Utils.Extensions.EnumExtensions;

namespace SysIgreja.Controllers
{

    [Authorize(Roles = Usuario.Master + "," + Usuario.Admin + "," + Usuario.Secretaria)]
    public class CentroCustoController : Controller
    {
        private readonly ICentroCustoBusiness centroCustoBusiness;

        public CentroCustoController(ICentroCustoBusiness centroCustoBusiness)
        {
            this.centroCustoBusiness = centroCustoBusiness;
        }

        public ActionResult Index()
        {
            ViewBag.Title = "Centros de Custo";
            ViewBag.Tipos = GetDescriptions<TiposCentroCustoEnum>().ToList();

            return View();
        }

        [HttpPost]
        public ActionResult GetCentroCustos()
        {
            var result = centroCustoBusiness
                .GetCentroCustos()
                .ToList()
                .Select(x => new CentroCustoViewModel
                {
                    Descricao = x.Descricao,
                    Id = x.Id,
                    Tipo = x.Tipo.GetDescription()
                });

            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetCentroCusto(int Id)
        {
            var result = centroCustoBusiness.GetCentroCustoById(Id);

            return Json(new { CentroCusto = result }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult PostCentroCusto(PostCentroCustoModel model)
        {
            centroCustoBusiness.PostCentroCusto(model);

            return new HttpStatusCodeResult(200);
        }

        [HttpPost]
        public ActionResult DeleteCentroCusto(int Id)
        {
            centroCustoBusiness.DeleteCentroCusto(Id);

            return new HttpStatusCodeResult(200);
        }
    }
}