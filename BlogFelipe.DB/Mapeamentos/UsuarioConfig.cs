using BlogFelipe.DB.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogFelipe.DB.Mapeamentos
{
    public class UsuarioConfig : EntityTypeConfiguration<Usuario>
    {
        public UsuarioConfig()
        {
//  Nome no banco
            ToTable("USUARIO");
//  
            HasKey(x => x.Id);

            Property(x => x.Id)
                .HasColumnName("IDUSUARIO")
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.sLogin)
                .HasColumnName("LOGIN")
                .HasMaxLength(30)
                .IsRequired();

            Property(x => x.sNome)
                .HasColumnName("NOME")
                .IsRequired()
                .HasMaxLength(100);

            Property(x => x.sSenha)
                .HasColumnName("SENHA")
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
