using FusionMVCProyectModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace FusionMVCProyectModel.Controllers
{
    public class BaseController : Controller
    {
        public AppUser CurrentUser
        {
            get
            {
                return new AppUser(this.User as ClaimsPrincipal);
            }
        }

        //public ApplicationUserManager UserManager
        //{
        //    get
        //    {
        //        return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
        //    }
        //    private set
        //    {
        //        _userManager = value;
        //    }
        //}


        //protected UsuarioApp UsuarioLogueado
        //{
        //    get
        //    {
        //        if (Identity == null)
        //        {
        //            return null;
        //        }
        //        return Identity;
        //    }
        //}



    }
}