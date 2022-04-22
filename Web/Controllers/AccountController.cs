using Core.Business.Account;
using Data.Context;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using SysIgreja.ViewModels;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Utils.Constants;
using Utils.Enums;
using Utils.Extensions;
using Utils.Services;

namespace SysIgreja.Controllers
{
    [Authorize(Roles = Usuario.Master + "," + Usuario.Admin)]
    public class AccountController : Controller
    {
        private readonly IAccountBusiness accountBusiness;

        public AccountController(IAccountBusiness accountBusiness)
            : this(new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
        {
            this.accountBusiness = accountBusiness;
        }

        public AccountController(UserManager<ApplicationUser> userManager)
        {
            UserManager = userManager;
        }

        public UserManager<ApplicationUser> UserManager { get; private set; }

        public ActionResult Index()
        {
            ViewBag.Title = "Usuários";

            return View();
        }

        [HttpPost]
        public ActionResult GetUsuarios()
        {
            var query = accountBusiness
                .GetUsuarios()
                .Where(x => x.Perfil != PerfisUsuarioEnum.Master)
                .ToList()
                .Select(x => new UsuarioViewModel
                {
                    Id = x.Id,
                    UserName = UtilServices.CapitalizarNome(x.UserName),
                    Perfil = x.Perfil.GetDescription(),
                    Status = x.Status.GetDescription(),
                    EquipanteId = x.EquipanteId
                });

            return Json(new { data = query }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetUsuario(string Id)
        {
            var result = accountBusiness.GetUsuarios().Select(x => new
            {
                Id = x.Id,
                Perfil = x.Perfil,
                Senha = x.Senha,
                EquipanteId = x.EquipanteId,
                UserName = x.UserName
            }).FirstOrDefault(x => x.Id == Id);

            return Json(new { Usuario = result }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetEquipantes(string Id)
        {
            var result = accountBusiness.GetEquipantesUsuario(Id).Select(x => new { x.Id, x.Nome });

            return Json(new { Equipantes = result }, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            accountBusiness.Seed();
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            model.EquipanteId = model.EquipanteId > 0 ? model.EquipanteId : null;

            if (string.IsNullOrEmpty(model.Id))
            {
                var user = new ApplicationUser() { UserName = model.UserName.ToLower(), EquipanteId = model.EquipanteId, Status = StatusEnum.Ativo, Perfil = model.Perfil, Senha = model.Password };
                UserManager.Create(user, model.Password);
                user = UserManager.FindByName(user.UserName);
                UserManager.AddToRole(user.Id, model.Perfil.GetDescription());
            }
            else
            {
                var user = UserManager.FindById(model.Id);
                user.UserName = model.UserName.ToLower();
                user.Senha = model.Password;
                user.Perfil = model.Perfil;
                user.EquipanteId = model.EquipanteId;
                var roles = UserManager.GetRoles(user.Id);
                UserManager.RemoveFromRoles(user.Id, roles.ToArray());
                UserManager.AddToRole(user.Id, model.Perfil.GetDescription());
                UserManager.Update(user);
                UserManager.ChangePassword(model.Id, model.OldPassword, model.Password);
            }

            return new HttpStatusCodeResult(200);
        }

        [HttpPost]
        public ActionResult ToggleUsuarioStatus(string Id)
        {
            accountBusiness.ToggleUsuarioStatus(Id);
            return new HttpStatusCodeResult(200);
        }

        [HttpPost]
        public ActionResult DeleteUsuario(string Id)
        {
            accountBusiness.DeleteUsuario(Id);
            return new HttpStatusCodeResult(200);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Disassociate(string loginProvider, string providerKey)
        {
            ManageMessageId? message = null;
            IdentityResult result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
            if (result.Succeeded)
            {
                message = ManageMessageId.RemoveLoginSuccess;
            }
            else
            {
                message = ManageMessageId.Error;
            }
            return RedirectToAction("Manage", new { Message = message });
        }
        //
        // POST: /Account/LinkLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LinkLogin(string provider)
        {
            // Request a redirect to the external login provider to link a login for the current user
            return new ChallengeResult(provider, Url.Action("LinkLoginCallback", "Account"), User.Identity.GetUserId());
        }

        //
        // GET: /Account/LinkLoginCallback
        public async Task<ActionResult> LinkLoginCallback()
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId());
            if (loginInfo == null)
            {
                return RedirectToAction("Manage", new { Message = ManageMessageId.Error });
            }
            var result = await UserManager.AddLoginAsync(User.Identity.GetUserId(), loginInfo.Login);
            if (result.Succeeded)
            {
                return RedirectToAction("Manage");
            }
            return RedirectToAction("Manage", new { Message = ManageMessageId.Error });
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult RemoveAccountList()
        {
            var linkedAccounts = UserManager.GetLogins(User.Identity.GetUserId());
            ViewBag.ShowRemoveButton = HasPassword() || linkedAccounts.Count > 1;
            return (ActionResult)PartialView("_RemoveAccountPartial", linkedAccounts);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && UserManager != null)
            {
                UserManager.Dispose();
                UserManager = null;
            }
            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
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

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            Error
        }

        private class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri) : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties() { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}