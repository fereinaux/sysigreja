using Core.Business.Etiquetas;
using Core.Business.Eventos;
using Core.Models.Etiquetas;
using System.Linq;
using System.Web.Mvc;
using Utils.Constants;

namespace SysIgreja.Controllers
{

    [Authorize(Roles = Usuario.Master + "," + Usuario.Admin + "," + Usuario.Secretaria)]
    public class EtiquetaController : Controller
    {
        private readonly IEventosBusiness eventosBusiness;
        private readonly IEtiquetasBusiness etiquetasBusiness;

        public EtiquetaController(IEventosBusiness eventosBusiness, IEtiquetasBusiness etiquetasBusiness)
        {
            this.eventosBusiness = eventosBusiness;
            this.etiquetasBusiness = etiquetasBusiness;
        }

        public ActionResult Index()
        {
            ViewBag.Title = "Marcadores";

            return View();
        }

        [HttpPost]
        public ActionResult GetEtiquetas()
        {
            var result = etiquetasBusiness
                .GetEtiquetas()
                .ToList()
                .Select(x => new
                {
                    Nome = x.Nome,
                    Id = x.Id,
                    Cor = x.Cor
                });

            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetEtiqueta(int Id)
        {
            var result = etiquetasBusiness.GetEtiquetaById(Id);

            return Json(new { Etiqueta = new { Id = result.Id, Nome = result.Nome, Cor = result.Cor } }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult PostEtiqueta(PostEtiquetaModel model)
        {
            etiquetasBusiness.PostEtiqueta(model);

            return new HttpStatusCodeResult(200);
        }

        [HttpPost]
        public ActionResult DeleteEtiqueta(int Id)
        {
            etiquetasBusiness.DeleteEtiqueta(Id);

            return new HttpStatusCodeResult(200);
        }
    }
}