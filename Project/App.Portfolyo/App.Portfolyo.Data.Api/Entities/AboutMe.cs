using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PortfolyoApp.Data.Api.Entities
{
    public class AboutMe : EntityBase
    {
        public string Introduction { get; set; } = default!;
        public string ImageUrl1 { get; set; } = default!;
        public string ImageUrl2 { get; set; } = default!;

        //Navigation Property
        public UserEntity User { get; set; } = default!;
    }
    internal class AboutMeConfiguration : IEntityTypeConfiguration<AboutMe>
    {
        public void Configure(EntityTypeBuilder<AboutMe> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Introduction).IsRequired().HasMaxLength(255);
            builder.Property(x => x.ImageUrl1).IsRequired().HasMaxLength(255);
            builder.Property(x => x.ImageUrl2).IsRequired().HasMaxLength(255);
        }
    }
}
