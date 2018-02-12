using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FusionMVCProyectModel.Models
{
    public class Login
    {
        [Display(Name = "Correo")]
        public string Email { get; set; }

        [Display(Name = "Clave de Acceso")]
        public string Password { get; set; }

        [Display(Name = "Recordarme")]
        public bool RememberMe { get; set; }

    }
}