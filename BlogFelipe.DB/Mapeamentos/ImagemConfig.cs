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
    public class ImagemConfig : EntityTypeConfiguration<Imagem>
    {
        public ImagemConfig()
        {
            ToTable("IMAGEM");

            HasKey(y => y.Id);

            Property(x => x.Id)
                .HasColumnName("IDIMAGEM")
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.sNome)
                .HasColumnName("NOME")
                .HasMaxLength(100)
                .IsRequired();

            Property(x => x.sExtensao)
                .HasColumnName("EXTENSAO")
                .HasMaxLength(10)
                .IsRequired();

            Property(x => x.bBytes)
                .IsRequired()
                .HasColumnName("BYTES");

            Property(x => x.idPost)
                .HasColumnName("IDPOST")
                .IsRequired();

            HasRequired(x => x.Post)
                .WithMany()
                .HasForeignKey(x => x.idPost);
        }
    }
}
