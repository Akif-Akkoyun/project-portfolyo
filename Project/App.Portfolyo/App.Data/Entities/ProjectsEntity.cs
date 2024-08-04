using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PortfolyoApp.Data.Entities
{
    public class ProjectsEntity : EntityBase
    {
        public string Title { get; set; } = string.Empty!;
        public string Description { get; set; } = string.Empty!;
        public string ImageUrl { get; set; } = string.Empty!;
        public string Url { get; set; } = string.Empty!;
        public string GithubUrl { get; set; } = string.Empty!;
        public string Tags { get; set; } = string.Empty!;
    }
    internal class ProjectsConfiguration : IEntityTypeConfiguration<ProjectsEntity>
    {
        public void Configure(EntityTypeBuilder<ProjectsEntity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Title).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Description).IsRequired().HasMaxLength(500);
            builder.Property(x => x.ImageUrl).IsRequired().HasMaxLength(255);
            builder.Property(x => x.Url).IsRequired().HasMaxLength(300);
            builder.Property(x => x.GithubUrl).IsRequired().HasMaxLength(300);
            builder.Property(x => x.Tags).IsRequired().HasMaxLength(300);
        }
    }
}
