using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace PortfolyoApp.Auth.Api.Data.Entites
{
    public class RoleEntity
    {
        public string Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        internal class RoleEntityConfiguration : IEntityTypeConfiguration<RoleEntity>
        {
            public void Configure(EntityTypeBuilder<RoleEntity> builder)
            {
                builder.HasKey(x => x.Id);
                builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            }
        }
    }
}
