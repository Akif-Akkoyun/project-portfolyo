using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortfolyoApp.Data;

namespace PortfolyoApp.Data.Entities
{
    public class UserEntity : EntityBase
    {
        public string UserName { get; set; } = default!;
        public string UserSurName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string PasswordHash { get; set; } = default!;
        public string? RefreshPasswordToken { get; set; }
        public int RoleId { get; set; }

        //nav prop
        public RoleEntity Role { get; set; } = default!;
    }
    public class UserEntityConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.Property(x => x.UserName).IsRequired().HasMaxLength(50);
            builder.Property(x => x.UserSurName).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(50);
            builder.Property(x => x.PasswordHash).IsRequired().HasMaxLength(255);


            builder.HasIndex(x => x.Email).IsUnique();
            builder.HasOne(d => d.Role)
           .WithMany()
           .HasForeignKey(d => d.RoleId)
           .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
