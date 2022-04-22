using Core.Business.Account;
<<<<<<< HEAD
using Core.Business.Configuracao;
=======
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
using Data.Context;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using SysIgreja.ViewModels;
<<<<<<< HEAD
=======
using System.Linq;
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Utils.Constants;
using Utils.Enums;
<<<<<<< HEAD
=======
using Utils.Extensions;
using Utils.Services;
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566

namespace SysIgreja.Controllers
{
    [Authorize(Roles = Usuario.Master + "," + Usuario.Admin)]
    public class LoginController : Controller
    {
        private readonly IAccountBusiness accountBusiness;
<<<<<<< HEAD
        private readonly IConfiguracaoBusiness configuracaoBusiness;

        public LoginController(IAccountBusiness accountBusiness, IConfiguracaoBusiness configuracaoBusiness)
            : this(new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
        {
            this.accountBusiness = accountBusiness;
            this.configuracaoBusiness = configuracaoBusiness;
=======

        public LoginController(IAccountBusiness accountBusiness)
            : this(new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
        {
            this.accountBusiness = accountBusiness;
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
        }

        public LoginController(UserManager<ApplicationUser> userManager)
        {
            UserManager = userManager;
        }

        public UserManager<ApplicationUser> UserManager { get; private set; }

        [AllowAnonymous]
        public ActionResult Index()
        {
<<<<<<< HEAD
            ViewBag.Configuracao = configuracaoBusiness.GetConfiguracao();
=======
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
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

<<<<<<< HEAD
                    return RedirectToAction("Index", "Home");
=======
                        return RedirectToAction("Index", "Home");                    
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
                }
                else
                    ModelState.AddModelError("", "Usuário e/ou senha inválidos.");

            }

<<<<<<< HEAD
            return View("Index", model);
=======
            return View("Index",model);
>>>>>>> 80495c8b8c10fef5b1b185455b7ef50cc662c566
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