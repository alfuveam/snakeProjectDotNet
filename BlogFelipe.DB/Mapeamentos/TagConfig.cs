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
    public class TagConfig : EntityTypeConfiguration<Tag>
    {
        public TagConfig()
        {
            ToTable("TAG");

            HasKey(x => x.sIDTag);

            Property(x => x.sIDTag)
                .HasColumnName("IDTAG")
                .HasMaxLength(20)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
        }
    }
}
