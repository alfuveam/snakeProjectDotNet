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
    public class PostConfig : EntityTypeConfiguration<Post>
    {
        public PostConfig()
        {
            ToTable("POST");

            HasKey(y => y.Id);

            Property(x => x.Id)
                .HasColumnName("IDPOST")
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.sAutor)
                .HasColumnName("AUTOR")
                .HasMaxLength(100)
                .IsRequired();

            Property(x => x.dDataPublicacao)
                .HasColumnName("DATAPUBLICACAO")
                .IsRequired();

            Property(x => x.sDescricao)
                .HasColumnName("DESCRICAO")                
                .IsRequired();
             
            Property(x => x.sResumo)
                .HasColumnName("RESUMO")
                .HasMaxLength(1000)
                .IsRequired();

            Property(x => x.sTitulo)
                .HasColumnName("TITULO")
                .HasMaxLength(100)
                .IsRequired();

            Property(x => x.bVisivel)
                .HasColumnName("VISIVEL")
				.IsRequired();

            HasMany(x => x.Arquivos)
                .WithOptional()
                .HasForeignKey(x => x.iDpost);

            HasMany(x => x.Comentarios)
                .WithOptional()
                .HasForeignKey(x => x.idPost);

            HasMany(x => x.Imagens)
                .WithOptional()
                .HasForeignKey(x => x.idPost);

            HasMany(x => x.Visitas)
                .WithOptional()
                .HasForeignKey(x => x.idPost);

            HasMany(x => x.PostTags)
                .WithOptional()
                .HasForeignKey(x => x.idPost);
        }
    }
}
