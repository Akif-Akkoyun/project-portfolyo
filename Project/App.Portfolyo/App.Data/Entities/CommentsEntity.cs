using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolyoApp.Data.Entities
{
    public class CommentsEntity : EntityBase
    {
        public long BlogPostId { get; set; }
        public long UserId { get; set; } 
        public string Content { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;

        public BlogPostEntity BlogPost { get; set; }= default!;
        public UserEntity User { get; set; } = default!;
    }
    public class CommentsEntityConfiguration : IEntityTypeConfiguration<CommentsEntity>
    {
        public void Configure(EntityTypeBuilder<CommentsEntity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.BlogPostId).IsRequired();
            builder.Property(x => x.UserId).IsRequired();
            builder.Property(x => x.Content).IsRequired();
            builder.Property(x => x.UserName).IsRequired();
            builder.HasOne(x => x.BlogPost).WithMany(x => x.Comments).HasForeignKey(x => x.BlogPostId);
            builder.HasOne(x => x.User).WithMany(x => x.Comments).HasForeignKey(x => x.UserId);
        }
    }
}
