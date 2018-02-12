using AppUsersModel;
using FusionMVCProyectModel.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace FusionMVCProyectModel.Controllers
{
    public class AccountController : BaseController
    {
        // GET: Account
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        // Post: Account
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Login data, string returnUrl)
        {
            ActionResult Result;

            var Repository = new AppUsersModel.Repository();
            var User = Repository.FindUser(data.Email, data.Password);

            if (User != null)
            {
                Result = SignInUser(User, data.RememberMe, returnUrl);
            }
            else
            {
                Result = View(data);
            }

            return Result;
        }

        private ActionResult SignInUser(User User, bool rememberMe, string returnUrl)
        {
            ActionResult Result;

            var Claims = new List<Claim>();


            Claims.Add(new Claim(ClaimTypes.NameIdentifier, User.ID.ToString()));
            Claims.Add(new Claim(ClaimTypes.Email, User.FirstName));
            Claims.Add(new Claim(ClaimTypes.Name, User.Email));
            Claims.Add(new Claim("FullName", $"{User.FirstName} {User.LastName}"));

            if (User.Roles != null && User.Roles.Any())
            {
                Claims.AddRange(User.Roles.Select(r => new Claim(ClaimTypes.Role, r.Name)));
            }

            var Identity = new ClaimsIdentity(Claims, DefaultAuthenticationTypes.ApplicationCookie);

            IAuthenticationManager AuthenticationManager = HttpContext.GetOwinContext().Authentication;

            AuthenticationManager.SignIn(new AuthenticationProperties
            {
                IsPersistent = rememberMe
            }, Identity);//SE GUARDA EN LA COKKIE DE AUTENTIFICACIÓN

            if (string.IsNullOrEmpty(returnUrl))
            {
                returnUrl = Url.Action("Index", "Home");
            }
            Result = Redirect(returnUrl);
            return Result;
        }

        public ActionResult LogOff()
        {

            IAuthenticationManager AuthenticationManager = HttpContext.GetOwinContext().Authentication;
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult ExternalLinkLogin(string provider, string returnUrl)
        //{
        //    string UserID = null;
        //    // Obtenemos el identificador del usuario autenticado             
        //    if (this.User.Identity.IsAuthenticated && User is ClaimsPrincipal)
        //    {
        //        var Identity = User as ClaimsPrincipal;
        //        var Claims = Identity.Claims.ToList();
        //        UserID = Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
        //    }
        //    // Solicitamos un Redirect al proveedor externo            
        //    return new ChallengeResult(provider, Url.Action("ExternalLinkLoginCallback", "Account",
        //        new { ReturnUrl = returnUrl }), UserID);
        //}

        //public async Task<ActionResult> ExternalLinkLoginCallback()
        //{
        //    ActionResult Result;
        //    // Obtener la información devuelta por el proveedor externo             
        //    var LoginInfo = await HttpContext.GetOwinContext().Authentication.
        //    GetExternalLoginInfoAsync(ChallengeResult.XsrfKey,
        //    User.Identity.GetUserId());

        //    if (LoginInfo == null)
        //    {
        //        Result = Content("No se pudo realizar la autenticación con el proveedor externo.");


        //    }
        //    else
        //    {
        //        // El usuario ha sido autenticado por el proveedor externo!                
        //        // Obtener la llave del proveedor de autenticación.                 
        //        // Esta llave es específica del usuario.                  
        //        string ProviderKey = LoginInfo.Login.ProviderKey;

        //        // Obtener el nombre del proveedor de autenticación.                 
        //        string ProviderName = LoginInfo.Login.LoginProvider;

        //        // Enlazar los datos de la cuenta externa con                  
        //        // la cuenta de usuario local.                 
        //        var Repository = new Repository();
        //        Repository.LinkExternalLogin(User.Identity.GetUserId<int>(), ProviderKey, ProviderName);
        //        Result = Content($"Se ha enlazado la cuenta local con la cuenta de {ProviderName}");
        //    }
        //    return Result;
        //}

    }
}