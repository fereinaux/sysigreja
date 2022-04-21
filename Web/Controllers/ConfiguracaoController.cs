using Core.Business.Configuracao;
using Core.Models.Configuracao;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Utils.Constants;
using Utils.Enums;
using static Utils.Extensions.EnumExtensions;

namespace SysIgreja.Controllers
{

    [Authorize(Roles = Usuario.Master + "," + Usuario.Admin + "," + Usuario.Secretaria)]
    public class ConfiguracaoController : Controller
    {
        private readonly IConfiguracaoBusiness configuracaoBusiness;

        public ConfiguracaoController(IConfiguracaoBusiness configuracaoBusiness)
        {
            this.configuracaoBusiness = configuracaoBusiness;
        }

        public ActionResult Index()
        {
            ViewBag.Title = "Parâmetros";

            return View();
        }

        [HttpGet]
        public ActionResult GetConfiguracao()
        {
            var result = configuracaoBusiness.GetConfiguracao();

            var jsonRes = Json(new { Configuracao = result }, JsonRequestBehavior.AllowGet);
            jsonRes.MaxJsonLength = Int32.MaxValue;
            return jsonRes;
        }

        [HttpGet]
        public ActionResult GetCampos()
        {
            var result = configuracaoBusiness.GetCampos();

            return Json(new { Campos = result }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult GetCamposEnum()
        {
            var result = GetDescriptions<CamposEnum>();

            return Json(new { Campos = result }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult PostCampos(List<CamposModel> campos)
        {
            configuracaoBusiness.PostCampos(campos);

            return new HttpStatusCodeResult(200);
        }

        [HttpPost]
        public ActionResult PostConfiguracao(PostConfiguracaoModel model)
        {
            configuracaoBusiness.PostConfiguracao(model);

            return new HttpStatusCodeResult(200);
        }

        [HttpPost]
        public ActionResult PostLogo(int id)
        {
            configuracaoBusiness.PostLogo(id);

            return new HttpStatusCodeResult(200);
        }


        [HttpPost]
        public ActionResult PostBackground(int id)
        {
            configuracaoBusiness.PostBackground(id);

            return new HttpStatusCodeResult(200);
        }
    }
}