using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolyoApp.Data.Entities
{
    public class FileEntity : EntityBase
    {
        public string FileName { get; set; } = default!;
        public string FilePath { get; set; } = default!;
    }
    public class FileConfiguration : IEntityTypeConfiguration<FileEntity>
    {
        public void Configure(EntityTypeBuilder<FileEntity> builder)
        {

            builder.Property(x => x.FileName).IsRequired().HasMaxLength(200);
            builder.Property(x => x.FilePath).IsRequired().HasMaxLength(255);
        }
    }

}
