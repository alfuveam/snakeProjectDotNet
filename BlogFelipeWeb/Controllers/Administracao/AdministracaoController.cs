using BlogFelipe.DB;
using BlogFelipe.DB.Classes;
using BlogFelipeWeb.Models.Administracao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogFelipeWeb.Controllers.Administracao
{
    [Authorize]
    public class AdministracaoController : Controller
    {
        //// GET: Administracao
        
        public ActionResult index()
        {
            return View();
        }

        public ActionResult CadastrarPost()
        {
            var viewModel = new CadastrarPostViewModel();
            viewModel.Autor = "Felipe Borges Tomaz";
            viewModel.DataPublicacao = DateTime.Now;
            viewModel.HoraPublicacao = DateTime.Now;
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult CadastrarPost(CadastrarPostViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var conexao = new ConexaoBanco();
                var post = new Post();

                //DateTime dDataPublicacao = DateTime.Now.Date;
                //DateTime dHoraPublicacao = DateTime.Now;
                //DateTime dConcatenar = dDataPublicacao + dHoraPublicacao;
                var dDataPublicao = new DateTime(
                        viewModel.DataPublicacao.Year,
                        viewModel.DataPublicacao.Month,
                        viewModel.DataPublicacao.Day,
                        viewModel.HoraPublicacao.Hour,
                        viewModel.HoraPublicacao.Minute,
                        viewModel.HoraPublicacao.Second
                    );

                post.sAutor = viewModel.Autor;
                post.dDataPublicacao = dDataPublicao;
                post.sDescricao = viewModel.Descricao;
                post.sResumo = viewModel.Resumo;
                post.sTitulo = viewModel.Titulo;
                post.bVisivel = viewModel.Visivel;
                post.PostTags = new List<TagPost>();

                if(viewModel.Tags != null)
                {
                    foreach (var item in viewModel.Tags)
                    {
                        var tagExiste = (from p in conexao.Tags where p.sIDTag.ToLower() == item.ToLower() select p).Any();

                        if (!tagExiste)
                        {
                            var tagClass = new Tag();
                            tagClass.sIDTag = item;
                            conexao.Tags.Add(tagClass);
                        }

                        var postTag = new TagPost();
                        postTag.sIdTag = item;
                        post.PostTags.Add(postTag);
                    }
                }

                try { 

                conexao.Posts.Add(post);                
                conexao.SaveChanges();
                return RedirectToAction("Index");
                }
                catch (Exception exp)
                {
                    ModelState.AddModelError("Erro Banco:", exp.Message);
                }

            }
            return View(viewModel);
        }


        public ActionResult EditarPost(int id)
        {
            var viewModel = new CadastrarPostViewModel();
            var conexao = new ConexaoBanco();
            var post = conexao.Posts.Where(x => x.Id == id).FirstOrDefault();

            if (post == null)
            {
                throw new Exception(string.Format("Post com código {0} não encontrado.", id));
            }

            viewModel.Titulo = post.sTitulo;
            viewModel.Autor = post.sAutor;
            viewModel.DataPublicacao = post.dDataPublicacao;
            viewModel.HoraPublicacao = post.dDataPublicacao;
            viewModel.Visivel = post.bVisivel;
            viewModel.Descricao = post.sDescricao;
            viewModel.Resumo = post.sResumo;
            viewModel.id = post.Id;

            viewModel.Tags = (from p in post.PostTags select p.sIdTag).ToList();

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult EditarPost(CadastrarPostViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var conexao = new ConexaoBanco();
                var post = conexao.Posts.Where(x => x.Id == viewModel.id).FirstOrDefault();

                post.sTitulo = viewModel.Titulo;
                post.sAutor = viewModel.Autor;
                var DataPublicacao = new DateTime(
                    viewModel.DataPublicacao.Year,
                    viewModel.DataPublicacao.Month,
                    viewModel.DataPublicacao.Day,
                    viewModel.HoraPublicacao.Hour,
                    viewModel.HoraPublicacao.Minute,
                    viewModel.HoraPublicacao.Second
                    );
                post.dDataPublicacao = DataPublicacao;
                post.bVisivel = viewModel.Visivel;
                post.sDescricao = viewModel.Descricao;
                post.sResumo = viewModel.Resumo;
                var postsTagsAtuais = post.PostTags.ToList();

                foreach (var item in postsTagsAtuais)
                {
                    conexao.TagPosts.Remove(item);
                }

                if (viewModel.Tags != null)
                {
                    foreach (var item in viewModel.Tags)
                    {
                        var tagExiste = (from p in conexao.Tags where p.sIDTag.ToLower() == item.ToLower() select p).Any();

                        if (!tagExiste)
                        {
                            var tagClass = new Tag();
                            tagClass.sIDTag = item;
                            conexao.Tags.Add(tagClass);
                        }

                        var postTag = new TagPost();
                        postTag.sIdTag = item;
                        post.PostTags.Add(postTag);
                    }
                }

                try
                {
                    conexao.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception exp) {
                    ModelState.AddModelError("Erro Banco: ", exp.Message);
                }
            }
            return View(viewModel);
        }

        #region
        public ActionResult ExcluirPost(int id)
        {
            var conexao = new ConexaoBanco();
            var post = (from p in conexao.Posts where p.Id == id select p).FirstOrDefault();

            if (post == null)
            {
                throw new Exception(string.Format("Post código {0} não exite.", id));
            }
            conexao.Posts.Remove(post);
            conexao.SaveChanges();

            return RedirectToAction("Index", "Blog");
        }
        #endregion
    }
}