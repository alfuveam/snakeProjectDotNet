using BlogFelipe.DB.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogFelipeWeb.Models.ContUsuario
{
    public class ListarPostsViewModel
    {
//  Registro por pagina = 10
        public List<Post> Posts { get; set; }
        public int iPaginaAtual { get; set; }
        public int iTotalDePaginas { get; set; }
        public string sTag { get; set; }
        public List<string> sTags { get; set; }
        public string sPesquisa { get; set; }
    }
}