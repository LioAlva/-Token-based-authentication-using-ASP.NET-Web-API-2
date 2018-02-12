using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Web.Mvc;

namespace FusionMVCProyectModel.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        [AllowAnonymous]
        // GET: Home
        public ActionResult Index()
        {
            var name = CurrentUser;

            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            var claims = identity.Claims;

            ClaimsPrincipal Principal = Thread.CurrentPrincipal as ClaimsPrincipal;

            if (Principal != null && Principal.Identity.IsAuthenticated)
            {
                var Claims = Principal.Claims.ToList();
                ViewBag.FullUserName = Claims.FirstOrDefault(c => c.Type == "FullName").Value;
            }

            bool IsAdministrator = Principal.IsInRole("Administrators");

            if (IsAdministrator)
            {
                ViewBag.FullUserName += " (Admin)";
            }

            return View();
        }

        public ActionResult AuthenticatedUsers()
        {
            return View();
        }
        [Authorize(Users = "Juan,Alejandra")]
        public ActionResult Payments()
        {

            return Content("<h1>Bienvenidos usuarios autorizados explicitamente </h1>");
        }

        [Authorize(Roles = "Administrators")]
        public ActionResult AdminUsers()
        {

            return Content("<h1>Bienvenidos Administradores</h1>");
        }

        [Authorize(Roles = "Human Resources, Administrators")]
        public ActionResult AdminAndRH()
        {
            return Content("<h1>¡Bienvenidos administradores y Recursos H.!</h1>");
        }

        public ActionResult GetClaims()
        {
            ClaimsPrincipal Principal = this.User as ClaimsPrincipal;

            var sb = new StringBuilder();

            if (Principal != null)
            {
                foreach (Claim c in Principal.Claims)
                {
                    sb.Append($"Tipo de Claim: {c.Type}, Valor: {c.Value}<br>");

                }
            }
            return Content(sb.ToString());
        }

        public ActionResult GetUserRoles()
        {
            ClaimsPrincipal Principal = this.User as ClaimsPrincipal;

            var sb = new StringBuilder(); if (Principal != null)
            {
                var Roles = Principal.Claims.
                Where(c => c.Type == ClaimTypes.Role).
                Select(c => c.Value).ToList();

                foreach (string Role in Roles)
                {
                    sb.Append($"{Role}<br>");
                }
            }
            return Content(sb.ToString());
        }



    }
}