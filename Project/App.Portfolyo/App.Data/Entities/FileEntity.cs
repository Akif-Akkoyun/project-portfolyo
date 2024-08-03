using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolyoApp.Data.Entities
{
    public class FileEntity
    {
        public int Id { get; set; }
        public string FileName { get; set; } = null!;
        public string FilePath { get; set; } = null!;

        internal class FileEntityConfiguration : IEntityTypeConfiguration<FileEntity>
        {
            public void Configure(EntityTypeBuilder<FileEntity> builder)
            {
                builder.HasKey(x => x.Id);
                builder.Property(x => x.FileName).IsRequired().HasMaxLength(50);
                builder.Property(x => x.FilePath).IsRequired().HasMaxLength(50);
            }
        }
    }
}
