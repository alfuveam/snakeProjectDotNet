using BlogFelipe.DB.Classes.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogFelipe.DB.Classes
{
    public class Visita : ClasseBase
    {
        public string sIP { get; set; }
        public DateTime dDataHora { get; set; }
        public int idPost { get; set; }

        public virtual Post Post { get; set; }
    }
}
