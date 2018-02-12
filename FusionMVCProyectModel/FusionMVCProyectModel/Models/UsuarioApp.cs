using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FusionMVCProyectModel.Models
{
    public class UsuarioApp
    {
        public int IdUsuario { get; set; }
        public string Email { get; set; }
        public string ClaveUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public string ApellidoUsuario { get; set; }
        public int ScoringUser { get; set; }
        public string ImgUrlFoto { get; set; }
        public string Permisos { get; set; }

        public UsuarioApp()
        {
        }
    }
}