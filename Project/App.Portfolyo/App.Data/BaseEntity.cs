using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PortfolyoApp.Data
{
    public abstract class EntityBase
    {
        public long Id { get; set; } = default!;
        public DateTime? CreatedAt { get; set; }
    }
    internal class EntityBaseConfiguration : IEntityTypeConfiguration<EntityBase>
    {
        public void Configure(EntityTypeBuilder<EntityBase> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.CreatedAt).IsRequired();
        }
    }
}
