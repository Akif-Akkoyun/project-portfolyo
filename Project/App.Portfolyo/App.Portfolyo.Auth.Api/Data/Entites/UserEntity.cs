using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PortfolyoApp.Auth.Api.Data.Entites
{
    public class UserEntity
    {
        public long Id { get; set; }
        public string UserName { get; set; } = default!;
        public string UserSurName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string PasswordHash { get; set; } = default!;
        public string RoleId { get; set; } = default!;
        public DateTime CreatedAt { get; set; }

        //nav prop
        public RoleEntity Role { get; set; } = default!;
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
            builder.Property(x => x.CreatedAt).IsRequired();

            
            builder.HasIndex(x => x.Email).IsUnique();
            builder.HasOne(x => x.Role).WithMany().HasForeignKey(x => x.RoleId).IsRequired();
        }
    }
}
 