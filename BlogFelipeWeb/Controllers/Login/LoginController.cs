using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogFelipeWeb.Controllers.Login
{
    //Comando para bloquar o acesso a usuarios nao habilitados
    [Authorize]
    public class LoginController : Controller
    {
        // GET: Login
        //  Para ter usuarios sem login
        [AllowAnonymous]
        public ActionResult Index(string ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }
    }
}