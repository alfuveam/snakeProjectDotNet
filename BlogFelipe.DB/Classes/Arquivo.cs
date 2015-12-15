using BlogFelipe.DB.Classes.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogFelipe.DB.Classes
{
    public class Arquivo : ClasseBase
    {        
        public string sNome { get; set; }
        public string sExtensao { get; set; }
        public byte[] bBytes{ get; set; }
        public int iDpost { get; set; }

        public virtual Post Post { get; set; }
    }
}
