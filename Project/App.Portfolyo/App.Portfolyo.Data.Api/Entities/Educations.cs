using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PortfolyoApp.Data.Api.Entities
{
    public class Educations : EntityBase
    {
        public string Degree { get; set; } = string.Empty!;
        public string School { get; set; } = string.Empty!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; } = string.Empty!;
    }
    internal class EducationsConfiguration : IEntityTypeConfiguration<Educations>
    {
        public void Configure(EntityTypeBuilder<Educations> builder)
        {
            builder.Property(e => e.Degree)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(e => e.School)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(e => e.StartDate)
                .IsRequired();
            builder.Property(e => e.EndDate)
                .IsRequired();
            builder.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(500);
        }
    }
}
