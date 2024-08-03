using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PortfolyoApp.Data.Api.Entities
{
    public class UserEntity : EntityBase
    {
        public string UserName { get; set; } = default!;
        public string UserSurName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string PasswordHash { get; set; } = default!;
        public int RoleId { get; set; }
    }
    internal class UserEntityConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.UserName).IsRequired().HasMaxLength(50);
            builder.Property(x => x.UserSurName).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(50);
            builder.Property(x => x.PasswordHash).IsRequired().HasMaxLength(255);
        }
    }

}
