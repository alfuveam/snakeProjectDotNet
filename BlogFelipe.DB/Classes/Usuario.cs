using BlogFelipe.DB.Classes.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogFelipe.DB.Classes
{
    public class Usuario : ClasseBase
    {
        public string sLogin { get; set; }
        public string sNome { get; set; }
        public string sSenha { get; set; }
    }
}
