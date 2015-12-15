using BlogFelipe.DB.Classes.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogFelipe.DB.Classes
{
    public class Post : ClasseBase
    {
//        public int idPost { get; set; }
        public string sAutor { get; set; }
        public DateTime dDataPublicacao { get; set; }
        public string sDescricao { get; set; }
        public string sResumo { get; set; }
        public string sTitulo { get; set; }
        public bool bVisivel { get; set; }

        //        public virtual Post post { get; set; }

        public virtual IList<Comentario> Comentarios { get; set; }
        public virtual IList<Arquivo> Arquivos { get; set; }
        public virtual IList<Imagem> Imagens { get; set; }
        public virtual IList<Visita> Visitas { get; set; }
        public virtual IList<TagPost> PostTags { get; set; }
    }
}
