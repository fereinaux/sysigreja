using Core.Business.Arquivos;
using Core.Business.Eventos;
using Core.Models.Arquivos;
using SysIgreja.ViewModels;
using System.Linq;
using System.Web.Mvc;
using Utils.Constants;

namespace SysIgreja.Controllers
{

    [Authorize(Roles = Usuario.Master + "," + Usuario.Admin + "," + Usuario.Secretaria)]
    public class ArquivoController : Controller
    {
        private readonly IEventosBusiness eventosBusiness;
        private readonly IArquivosBusiness arquivosBusiness;

        public ArquivoController(IEventosBusiness eventosBusiness, IArquivosBusiness arquivosBusiness)
        {
            this.eventosBusiness = eventosBusiness;
            this.arquivosBusiness = arquivosBusiness;
        }

        public ActionResult Index()
        {
            ViewBag.Title = "Arquivos";

            return View();
        }

        [HttpPost]
        public ActionResult GetArquivos()
        {
            var query = arquivosBusiness.GetArquivos();

            return MapAqruivos(query);
        }

        [HttpPost]
        public ActionResult GetArquivosLancamento(int Id)
        {
            var query = arquivosBusiness.GetArquivosByLancamento(Id);

            return MapAqruivos(query);
        }

        [HttpPost]
        public ActionResult GetArquivosEquipanteEvento(int eventoid,int equipanteid)
        {
            var query = arquivosBusiness.GetArquivosByEquipanteEvento(equipanteid,eventoid );

            return MapAqruivos(query);
        }

        [HttpPost]
        public ActionResult GetArquivosParticipante(int participanteId)
        {
            var query = arquivosBusiness.GetArquivosByParticipante(participanteId);

            return MapAqruivos(query);
        }

        [HttpPost]
        public ActionResult GetArquivosEvento(int Id)
        {
            var query = arquivosBusiness.GetArquivosByEvento(Id);

            return MapAqruivos(query);
        }

        private ActionResult MapAqruivos(IQueryable<Data.Entities.Arquivo> query)
        {
            var result = query.ToList()
                .Select(x => new
                {
                    Id = x.Id,
                    Nome = x.Nome,
                    Extensao = x.Extensao
                });

            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetArquivo(int Id)
        {
            var arquivo = arquivosBusiness.GetArquivoById(Id);

            return File(arquivo.Conteudo, arquivo.Tipo, arquivo.Nome);
        }       

        [HttpPost]
        public int PostArquivo(PostArquivoModel model)
        {
            model.EventoId = model.EventoId.HasValue ? model.EventoId.Value : eventosBusiness.GetEventoAtivo().Id;

            return arquivosBusiness.PostArquivo(model);
        }

        [HttpPost]
        public ActionResult DeleteArquivo(int Id)
        {
            arquivosBusiness.DeleteArquivo(Id);

            return new HttpStatusCodeResult(200);
        }

        [HttpPost]
        public ActionResult DeleteFotoParticipante(int Id)
        {
            arquivosBusiness.DeleteFotoParticipante(Id);

            return new HttpStatusCodeResult(200);
        }

        [HttpPost]
        public ActionResult DeleteFotoEquipante(int Id)
        {
            arquivosBusiness.DeleteFotoEquipante(Id);

            return new HttpStatusCodeResult(200);
        }
    }
}