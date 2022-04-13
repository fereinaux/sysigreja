using Core.Business.ContaBancaria;
using Core.Models.ContaBancaria;
using SysIgreja.ViewModels;
using System.Linq;
using System.Web.Mvc;
using Utils.Constants;
using Utils.Enums;
using static Utils.Extensions.EnumExtensions;

namespace SysIgreja.Controllers
{

    [Authorize(Roles = Usuario.Master + "," + Usuario.Admin + "," + Usuario.Secretaria)]
    public class ContaBancariaController : Controller
    {
        private readonly IContaBancariaBusiness contaBancariaBusiness;

        public ContaBancariaController(IContaBancariaBusiness contaBancariaBusiness)
        {
            this.contaBancariaBusiness = contaBancariaBusiness;
        }

        public ActionResult Index()
        {
            ViewBag.Title = "Contas Bancárias";
            ViewBag.Bancos = GetDescriptions<BancosEnum>().ToList();

            return View();
        }

        [HttpPost]
        public ActionResult GetContasBancarias()
        {
            var result = contaBancariaBusiness
                .GetContasBancarias()
                .ToList()
                .Select(x => new ContaBancariaViewModel {
                    Id = x.Id,
                    Banco = x.Banco.GetDescription(),
                    Agencia = x.Agencia,
                    CPF = x.CPF,
                    Conta = x.Conta,
                    Nome = x.Nome,
                    Operacao = x.Operacao
                });

            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetContaBancaria(int Id)
        {
            var result = contaBancariaBusiness.GetContaBancariaById(Id);

            return Json(new { ContaBancaria = result }, JsonRequestBehavior.AllowGet);
        }        

        [HttpPost]
        public ActionResult PostContaBancaria(PostContaBancariaModel model)
        {
            contaBancariaBusiness.PostContaBancaria(model);

            return new HttpStatusCodeResult(200);
        }

        [HttpPost]
        public ActionResult DeleteContaBancaria(int Id)
        {
            contaBancariaBusiness.DeleteContaBancaria(Id);

            return new HttpStatusCodeResult(200);
        }
    }
}