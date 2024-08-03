using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PortfolyoApp.Data.Api.Entities
{
    public class ContactMessages : EntityBase
    {
        public string Name { get; set; } = string.Empty!;
        public string Email { get; set; } = string.Empty!;
        public string Message { get; set; } = string.Empty!;
        public string Subject { get; set; } = string.Empty!;
        public DateTime SentDate { get; set; }
        public byte IsRead { get; set; }
        public string Reply { get; set; } = string.Empty!;
        public DateTime ReplyDate { get; set; }
    }
    internal class ContactMessagesConfiguration : IEntityTypeConfiguration<ContactMessages>
    {
        public void Configure(EntityTypeBuilder<ContactMessages> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Message).IsRequired().HasMaxLength(500);
            builder.Property(x => x.Subject).IsRequired().HasMaxLength(100);
            builder.Property(x => x.SentDate).IsRequired();
            builder.Property(x => x.IsRead).IsRequired();
            builder.Property(x => x.Reply).IsRequired().HasMaxLength(500);
            builder.Property(x => x.ReplyDate).IsRequired();
        }
    }
}
