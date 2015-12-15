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
            viewModel.Posts = (from p in posts orderby p.dDataPublicacao descending select p).Skip(qtdeRegistrosPular).Take(registroPorPagina).ToList();
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
        public ActionResult Post(int id)
        {
            var conexao = new ConexaoBanco();
            var viewModel = new DetalhesPostViewModel();
            var post = conexao.Posts.Where(x => x.Id == id).FirstOrDefault();

            viewModel.id = post.Id;
            viewModel.Titulo = post.sTitulo;
            viewModel.Autor = post.sAutor;
            viewModel.DataPublicacao = post.dDataPublicacao;
            viewModel.HoraPublicacao = post.dDataPublicacao;
            viewModel.Visivel = post.bVisivel;
            viewModel.Descricao = post.sDescricao;
            viewModel.Resumo = post.sResumo;

            viewModel.Tags = (from p in post.PostTags select p.sIdTag).ToList();

            return View(viewModel);
        }
        #endregion

    }
}