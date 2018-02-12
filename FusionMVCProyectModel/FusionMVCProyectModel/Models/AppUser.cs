using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace FusionMVCProyectModel.Models
{
    public class AppUser : ClaimsPrincipal
    {
        public AppUser(ClaimsPrincipal principal)
        : base(principal)
        {
        }

        public string Name
        {
            get
            {
                return this.FindFirst(ClaimTypes.Name).Value;
            }
        }

        public string Email
        {
            get
            {
                return this.FindFirst(ClaimTypes.Email).Value;
            }
        }
        public List<string> GetRols
        {
            get
            {
                return this.Claims.Where(c => c.Type == ClaimTypes.Role).
                Select(c => c.Value).ToList();
            }
        }

        //Claims.Where(c => c.Type == ClaimTypes.Role).
        //        Select(c => c.Value).ToList();

        //var Roles = Principal.Claims.
        //        Where(c => c.Type == ClaimTypes.Role).
        //        Select(c => c.Value).ToList();
    }
}