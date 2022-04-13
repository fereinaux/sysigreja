using Arquitetura.ViewModels;
using AutoMapper;
using Core.Business.Arquivos;
using Core.Business.Eventos;
using Core.Models.Eventos;
using SysIgreja.ViewModels;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using Utils.Constants;
using Utils.Enums;
using Utils.Extensions;

namespace SysIgreja.Controllers
{

    [Authorize(Roles = Usuario.Master + "," + Usuario.Admin + "," + Usuario.Secretaria)]
    public class EventoController : Controller
    {
        private readonly IEventosBusiness eventosBusiness;
        private readonly IArquivosBusiness arquivosBusiness;
        private readonly IMapper mapper;

        public EventoController(IEventosBusiness eventosBusiness, IArquivosBusiness arquivosBusiness)
        {
            this.eventosBusiness = eventosBusiness;
            this.arquivosBusiness = arquivosBusiness;
            mapper = new MapperRealidade().mapper;
        }

        public ActionResult Index()
        {
            ViewBag.Title = "Eventos";

            return View();
        }

        [HttpGet]
        public ActionResult GetTipos()
        {
            return Json(new { Tipos = EnumExtensions.GetDescriptions<TiposEventoEnum>().ToList() }, JsonRequestBehavior.AllowGet);            
        }

        [HttpPost]
        public ActionResult GetEventos()
        {
            var result = eventosBusiness.GetEventos()
                .ToList()
                .Select(x => new EventoViewModel
                {
                    Id = x.Id,
                    DataEvento = x.DataEvento,
                    Numeracao = x.Numeracao,
                    Capacidade = x.Capacidade,
                    TipoEvento = x.TipoEvento.GetDescription(),
                    Status = x.Status.GetDescription(),
                    Valor = x.Valor.ToString("C", CultureInfo.CreateSpecificCulture("pt-BR")),
                    QtdAnexos = arquivosBusiness.GetArquivosByEvento(x.Id).Count()
                });

            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetEvento(int Id)
        {
            var result = eventosBusiness.GetEventoById(Id);



            return Json(new { Evento = mapper.Map<PostEventoModel>(result) }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ToggleEventoStatus(int Id)
        {
            if (eventosBusiness.ToggleEventoStatus(Id))
            {
                return new HttpStatusCodeResult(200);
            }

            return new HttpStatusCodeResult(400, Mensagens.EventoAberto);

        }

        [HttpPost]
        public ActionResult PostEvento(PostEventoModel model)
        {
            eventosBusiness.PostEvento(model);

            return new HttpStatusCodeResult(200);
        }

        [HttpPost]
        public ActionResult DeleteEvento(int Id)
        {
            eventosBusiness.DeleteEvento(Id);

            return new HttpStatusCodeResult(200);
        }
    }
}