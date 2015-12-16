
using BlogFelipe.DB;
using BlogFelipeWeb.Models.Administracao;
using System;
using System.Web.Mvc;

using BlogFelipe.DB.Classes;
using System.Linq;

namespace BlogFelipeWeb.Controllers.ContUsuario
{
    [Authorize]
    public class ContUsuarioController : Controller
    {
        // GET: Usuario
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CadastrarUsuario()
        {
            var viewModel = new CadastrarUsuarioViewModel();

            return View(viewModel);
        }
        
        [HttpPost]
        public ActionResult CadastrarUsuario(CadastrarUsuarioViewModel viewModel)
        {
            if (ModelState.IsValid) {
                var conexao = new ConexaoBanco();
                var usuario = new Usuario();

                usuario.sLogin = viewModel.sLogin;
                usuario.sNome = viewModel.sNome;
                usuario.sSenha = viewModel.sSenha;

                var acho = conexao.Usuarios.Where(x => x.sNome == usuario.sNome).FirstOrDefault();

                if (acho == null)
                 {
                    try
                    {
                        conexao.Usuarios.Add(usuario);
                        conexao.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    catch (Exception exp)
                    {
                        ModelState.AddModelError("Erro Banco:", exp.Message);
                    }
                }
                else
                {
                    throw new Exception(string.Format("Usuario já Cadastrado"));
                }
            }
            return View(viewModel);
        }

        public ActionResult EditarUsuario(int id)
        {
            var viewModel = new CadastrarUsuarioViewModel();
            var conexao = new ConexaoBanco();
            var usuario = conexao.Usuarios.Where(x => x.Id == id).FirstOrDefault();

            if (usuario == null)
            {
                throw new Exception(string.Format("Posto com código {0} não encontrado.", id));
            }
            viewModel.iId = usuario.Id;
            viewModel.sLogin = usuario.sLogin;
            viewModel.sNome = usuario.sNome;
            viewModel.sSenha = usuario.sSenha;
            
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult EditarUsuario(CadastrarUsuarioViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var conexao = new ConexaoBanco();
                var usuarios = conexao.Usuarios.Where(x => x.Id == viewModel.iId).FirstOrDefault();

                usuarios.Id = viewModel.iId;
                usuarios.sLogin = viewModel.sLogin;
                usuarios.sNome = viewModel.sNome;
                usuarios.sSenha = viewModel.sSenha;

                //var acho = conexao.Usuarios.Where(x => x.sNome == viewModel.sNome && x.Id == viewModel.iId).FirstOrDefault();
                var acho = (from p in conexao.Usuarios where p.Id != viewModel.iId && p.sNome == viewModel.sNome  select p).FirstOrDefault();

                if (acho == null)
                {
                    try
                    {
                        //conexao.Usuarios.Add(usuarios);
                        conexao.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    catch (Exception exp)
                    {
                        ModelState.AddModelError("Erro Banco:", exp.Message);
                    }                    
                }
                else
                {
                    throw new Exception(string.Format("Usuario já cadastrado!"));
                }
            }
            return View(viewModel);
        }

        #region
        public ActionResult ExcluirUsuario(int id)
        {
            var conexao = new ConexaoBanco();
            var usuario = (from p in conexao.Usuarios where p.Id == id select p).FirstOrDefault();

            if (usuario == null)
            {
                throw new Exception(string.Format("Post código {0} não exite.", id));
            }
            conexao.Usuarios.Remove(usuario);
            conexao.SaveChanges();

            return RedirectToAction("Index", "Administracao");
        }
        #endregion
    }
}