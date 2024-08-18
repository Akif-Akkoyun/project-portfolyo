using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace PortfolyoApp.Data.Entities
{
    public class RoleEntity
    {
        public int Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public class RoleEntityConfiguration : IEntityTypeConfiguration<RoleEntity>
        {
            public void Configure(EntityTypeBuilder<RoleEntity> builder)
            {
                builder.HasKey(x => x.Id);
                builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            }
        }
    }
}
