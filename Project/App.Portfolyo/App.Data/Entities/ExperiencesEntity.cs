using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PortfolyoApp.Data.Entities
{
    public class ExperiencesEntity : EntityBase
    {
        public string Title { get; set; } = default!;
        public string Company { get; set; } = default!;
        public string StartDate { get; set; } = default!;
        public string EndtDate { get; set; } = default!;
        public string Description { get; set; } = default!;
    }
    internal class ExperiencesConfiguration : IEntityTypeConfiguration<ExperiencesEntity>
    {
        public void Configure(EntityTypeBuilder<ExperiencesEntity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Title).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Company).IsRequired().HasMaxLength(100);
            builder.Property(x => x.StartDate).IsRequired().HasMaxLength(100);
            builder.Property(x => x.EndtDate).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Description).IsRequired().HasMaxLength(1000);
        }
    }
}
