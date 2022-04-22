using Core.Business.Account;
using Core.Business.Configuracao;
using Data.Context;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using SysIgreja.ViewModels;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Utils.Constants;
using Utils.Enums;

namespace SysIgreja.Controllers
{
    [Authorize(Roles = Usuario.Master + "," + Usuario.Admin)]
    public class LoginController : Controller
    {
        private readonly IAccountBusiness accountBusiness;
        private readonly IConfiguracaoBusiness configuracaoBusiness;

        public LoginController(IAccountBusiness accountBusiness, IConfiguracaoBusiness configuracaoBusiness)
            : this(new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
        {
            this.accountBusiness = accountBusiness;
            this.configuracaoBusiness = configuracaoBusiness;
        }

        public LoginController(UserManager<ApplicationUser> userManager)
        {
            UserManager = userManager;
        }

        public UserManager<ApplicationUser> UserManager { get; private set; }

        [AllowAnonymous]
        public ActionResult Index()
        {
            ViewBag.Configuracao = configuracaoBusiness.GetConfiguracao();
            accountBusiness.Seed();
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindAsync(model.UserName.ToLower(), model.Password.ToLower());
                if ((user != null) && (user.Status == StatusEnum.Ativo))
                {
                    await SignInAsync(user, model.RememberMe);

                    return RedirectToAction("Index", "Home");
                }
                else
                    ModelState.AddModelError("", "Usuário e/ou senha inválidos.");

            }

            return View("Index", model);
        }


        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
        }

    }
}