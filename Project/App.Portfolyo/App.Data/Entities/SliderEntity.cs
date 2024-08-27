using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolyoApp.Data.Entities
{
    public class SliderEntity : EntityBase
    {
        public string ImgUrl1 { get; set; } = string.Empty;
        public string ImgUrl2 { get; set; } = string.Empty;
    }
    internal class SliderConfiguration : IEntityTypeConfiguration<SliderEntity>
    {
        public void Configure(EntityTypeBuilder<SliderEntity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.ImgUrl1).IsRequired().HasMaxLength(255);
            builder.Property(x => x.ImgUrl2).IsRequired().HasMaxLength(255);
        }
    }
}
