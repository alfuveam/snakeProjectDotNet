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
    public class ArquivoConfig : EntityTypeConfiguration<Arquivo>
    {
        public ArquivoConfig()
        {
            ToTable("ARQUIVO");

            HasKey(x => x.Id);

            Property(x => x.Id)
                .HasColumnName("IDARQUIVO")
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.sNome)
                .HasColumnName("NOME")
                .HasMaxLength(100)
                .IsRequired();

            Property(x => x.sExtensao)
                .HasColumnName("EXTENSAO")
                .IsRequired();

            Property(x => x.bBytes)
                .IsRequired()
                .HasColumnName("BYTES");

            Property(x => x.iDpost)
                .HasColumnName("IDPOST");

//  Chave estrangeira
            HasRequired(x => x.Post)
                .WithMany()
                .HasForeignKey(x => x.iDpost);

        }
    }
}
