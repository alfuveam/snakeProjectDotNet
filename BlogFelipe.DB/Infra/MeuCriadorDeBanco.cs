using BlogFelipe.DB.Classes;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogFelipe.DB.Infra
{
    public class MeuCriadorDeBanco : DropCreateDatabaseAlways<ConexaoBanco>
    {
        protected override void Seed(ConexaoBanco context)
        {
            context.Usuarios.Add(new Usuario { sLogin = "ADM", sNome = "Administrador", sSenha = "admin"});
            base.Seed(context);
        }
    }
}
