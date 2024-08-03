using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PortfolyoApp.Data.Api.Entities
{
    public class PersonalInfo : EntityBase
    {
        public string About { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Surname { get; set; } = default!;
        public DateTime BirtDday { get; set; } = default!;
    }
    internal class PersonalInfoConfiguration : IEntityTypeConfiguration<PersonalInfo>
    {
        public void Configure(EntityTypeBuilder<PersonalInfo> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.About).IsRequired().HasMaxLength(300);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Surname).IsRequired().HasMaxLength(50);
            builder.Property(x => x.BirtDday).IsRequired();
        }
    }
}
