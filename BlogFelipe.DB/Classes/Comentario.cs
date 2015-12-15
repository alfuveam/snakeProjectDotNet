using BlogFelipe.DB.Classes.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogFelipe.DB.Classes
{
    public class Comentario : ClasseBase
    {
        public string lDescricao { get; set; }
        public bool bAdmPost { get; set; }
        public string sEmail { get; set; }
        public string sPaginaWeb { get; set; }
        public string sNome { get; set; }
        public int idPost { get; set; }

        public virtual Post Post { get; set; }
    }
}
