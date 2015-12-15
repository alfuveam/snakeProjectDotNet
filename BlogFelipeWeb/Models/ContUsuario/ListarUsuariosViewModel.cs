using BlogFelipe.DB.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogFelipeWeb.Models.ContUsuario
{
    public class ListarUsuariosViewModel
    {
        public List<Usuario> Usuarios { get; set; }
        public int iPaginaAtual { get; set; }
        public int iTotalDePaginas { get; set; }
    }
}