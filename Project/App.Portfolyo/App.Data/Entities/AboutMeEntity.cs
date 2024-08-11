using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PortfolyoApp.Data.Entities
{
    public class AboutMeEntity : EntityBase
    {
        public string Introduction { get; set; } = default!;
        public string ImageUrl1 { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string DateOfbirth { get; set; }= default!;
        public string Address { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public int ZipCode { get; set; }
    }
    internal class AboutMeConfiguration : IEntityTypeConfiguration<AboutMeEntity>
    {
        public void Configure(EntityTypeBuilder<AboutMeEntity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Introduction).IsRequired().HasMaxLength(255);
            builder.Property(x => x.ImageUrl1).IsRequired().HasMaxLength(255);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(255);
            builder.Property(x => x.DateOfbirth).IsRequired().HasMaxLength(255);
            builder.Property(x => x.Address).IsRequired().HasMaxLength(255);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(255);
            builder.Property(x => x.PhoneNumber).IsRequired().HasMaxLength(255);
            builder.Property(x => x.ZipCode).IsRequired();
        }
    }
}
