using BlogFelipe.DB.Classes.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogFelipe.DB.Classes
{
    public class TagPost : ClasseBase
    {
//        public int idTagPost { get; set; }
        public string sIdTag { get; set; }
        public int idPost { get; set; }

        public virtual Post Post { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
