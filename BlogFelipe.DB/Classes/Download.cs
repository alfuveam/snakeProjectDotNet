using BlogFelipe.DB.Classes.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogFelipe.DB.Classes
{
    public class Download : ClasseBase
    {
        public string sIP { get; set; }
        public DateTime dDataHora { get; set; }
        public int iDArquivo { get; set; }

        public virtual Arquivo Arquivo { get; set; }
    }
}
