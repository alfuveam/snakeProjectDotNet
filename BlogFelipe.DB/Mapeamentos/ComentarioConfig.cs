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
    public class ComentarioConfig : EntityTypeConfiguration<Comentario>
    {
        public ComentarioConfig()
        {
            ToTable("COMENTARIO");

            HasKey(x => x.Id);

            Property(x => x.Id)
                .HasColumnName("IDCOMENTARIO")
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.lDescricao)
                .HasColumnName("DESCRICAO")
//  Devido a cada banco ter um tamanho diferente                
                .IsMaxLength()
                .IsRequired();

            Property(x => x.bAdmPost)
                .HasColumnName("ADMPOST")
                .IsRequired();

            Property(x => x.sEmail)
                .HasColumnName("EMAIL")
                .HasMaxLength(100);                

            Property(x => x.sPaginaWeb)
                .HasColumnName("PAGINAWEB")
                .HasMaxLength(100);                

            Property(x => x.sNome)
                .HasColumnName("NOME")
				.HasMaxLength(100)
                .IsRequired();

            Property(x => x.idPost)
                .HasColumnName("IDPOST")
				.IsRequired();

            Property(x => x.dDataHora)
                .HasColumnName("DATAHORA")
                .IsRequired();

            HasRequired(x => x.Post)
                .WithMany()
                .HasForeignKey(x => x.idPost);
        }
    }
}
