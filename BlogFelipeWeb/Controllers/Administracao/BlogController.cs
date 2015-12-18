using BlogFelipe.DB;
using BlogFelipe.DB.Classes;
using BlogFelipeWeb.Models.Administracao;
using BlogFelipeWeb.Models.ContUsuario;
using BlogFelipeWeb.Models.Detalhes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogFelipeWeb.Controllers.Administracao
{
    public class BlogController : Controller
    {
        // GET: Blog, string aceita nulo com padrão,
        public ActionResult Index(int? pagina, string tag, string pesquisa)
        {
            var conexao = new ConexaoBanco();

//  Se não for nulo
            var paginaCorreta = pagina.GetValueOrDefault(1);
            var registroPorPagina = 10;
            var posts = (from p in conexao.Posts where p.bVisivel select p);

//  Verificando se é nulo a tag
            if (!string.IsNullOrEmpty(tag))
            {
                //  Any acha o primiero valor, 
                posts = (from p in posts where p.PostTags.Any(x => x.sIdTag.ToUpper() == tag.ToUpper()) select p);
            }

//  Verifica quando não é nulo e nem vazio a pesquisa
            if (!string.IsNullOrEmpty(pesquisa))
            {
                posts = (from p in posts where p.sTitulo.ToUpper().Contains(pesquisa.ToUpper())
                        ||  p.sResumo.ToUpper().Contains(pesquisa.ToUpper())
                        ||  p.sDescricao.ToUpper().Contains(pesquisa.ToUpper())
                         select p);
            }

            var qtdeRegistros = posts.Count();
            var indiceDaPagina = paginaCorreta - 1;
            var qtdeRegistrosPular = (indiceDaPagina * registroPorPagina);
            //  Arendonta para cima
            var qtdePaginas = Math.Ceiling((decimal)qtdeRegistros / (decimal)registroPorPagina);

            var viewModel = new ListarPostsViewModel();
            viewModel.Posts = (from p in posts orderby p.dDataPublicacao descending select new DetalhesPostViewModel
            {
                DataPublicacao = p.dDataPublicacao,
                Autor = p.sAutor,
                Descricao = p.sDescricao,
                id = p.Id,
                Resumo = p.sResumo,
                Titulo = p.sTitulo,
                Visivel = p.bVisivel,
                QtdComentarios = p.Comentarios.Count
            }
            ).Skip(qtdeRegistrosPular).Take(registroPorPagina).ToList();


            viewModel.iPaginaAtual = paginaCorreta;
            viewModel.iTotalDePaginas = (int)qtdePaginas;
            viewModel.sTag = tag;

            viewModel.sTags = (from p in conexao.Tags where conexao.TagPosts.Any(x => x.sIdTag == p.sIDTag) orderby p.sIDTag ascending select p.sIDTag).ToList();

            viewModel.sPesquisa = pesquisa;
            return View(viewModel);
        }

        public ActionResult _Paginacao()
        {
            return PartialView();
        }

        #region
        public ActionResult Post(int id, int? pagina)
        {
            var conexao = new ConexaoBanco();
            var viewModel = new DetalhesPostViewModel();
            var post = (from p in conexao.Posts where p.Id == id select p).FirstOrDefault();

            if (post == null)
            {
                throw new Exception(string.Format("Post Código {0} não encontrado"));
            }
            preencherViewModel(viewModel, post, pagina);


            viewModel.Tags = (from p in post.PostTags select p.sIdTag).ToList();

            return View(viewModel);
        }

        private static void preencherViewModel(DetalhesPostViewModel viewModel, Post post, int? pagina)
        {
            viewModel.id = post.Id;
            viewModel.Titulo = post.sTitulo;
            viewModel.Autor = post.sAutor;
            viewModel.DataPublicacao = post.dDataPublicacao;
            viewModel.HoraPublicacao = post.dDataPublicacao;
            viewModel.Visivel = post.bVisivel;
            viewModel.Descricao = post.sDescricao;
            viewModel.Resumo = post.sResumo;
            viewModel.QtdComentarios = post.Comentarios.Count();
            viewModel.Descricao = post.sDescricao;
            //Adicionar tags
            viewModel.Tags = post.PostTags.Select(x => x.sIdTag).ToList();
            var paginaCorreta = pagina.GetValueOrDefault(1);
            var registroPorPagina = 10;
            var qtdeRegistros = post.Comentarios.Count();
            var indiceDaPagina = paginaCorreta - 1;
            var qtdeRegistrosPular = (indiceDaPagina * registroPorPagina);
            //  Arendonta para cima
            var qtdePaginas = Math.Ceiling((decimal)qtdeRegistros / (decimal)registroPorPagina);

            viewModel.iLComentarios = (from p in post.Comentarios orderby p.dDataHora descending select p)
            .Skip(qtdeRegistrosPular).Take(registroPorPagina).ToList();

            viewModel.iPaginaAtual = paginaCorreta;
            viewModel.iTotalPaginas = (int)qtdePaginas;
        }
        #endregion

        #region
        [HttpPost]
        public ActionResult Post(DetalhesPostViewModel viewModel)
        {
            var conexao = new ConexaoBanco();
            var post = (from p in conexao.Posts where p.Id == viewModel.id select p).FirstOrDefault();

            if (ModelState.IsValid)
            {

                if (post == null)
                {
                    throw new Exception(string.Format("Post código {0} não encontrado.", viewModel.id));
                }
                var comentario = new Comentario();
                comentario.bAdmPost = HttpContext.User.Identity.IsAuthenticated;
                comentario.lDescricao = viewModel.ComentarioDescricao;
                comentario.sEmail = viewModel.ComentarioEmail;
                comentario.idPost = viewModel.id;
                comentario.sNome = viewModel.ComentarioNome;
                comentario.sPaginaWeb = viewModel.ComentarioPaginaWeb;
                comentario.dDataHora = DateTime.Now;

                try
                {
                    conexao.Comentarios.Add(comentario);
                    conexao.SaveChanges();
                    return Redirect(Url.Action("Post", new
                    {
                        ano = post.dDataPublicacao.Year,
                        mes = post.dDataPublicacao.Month,
                        dia = post.dDataPublicacao.Day,
                        titulo = post.sTitulo,
                        id = post.Id
                    }) + "#comentarios");
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }
            preencherViewModel(viewModel, post, null);
            return View(viewModel);
        }
        #endregion

        public ActionResult _PaginacaoPost()
        {
            return PartialView();
        }
    }
}