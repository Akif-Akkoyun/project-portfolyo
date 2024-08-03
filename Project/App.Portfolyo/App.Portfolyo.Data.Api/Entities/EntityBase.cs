using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PortfolyoApp.Data.Api.Entities
{
    public class EntityBase
    {
        public int Id { get; set; }
    }
    internal class EntityBaseConfiguration : IEntityTypeConfiguration<EntityBase>
    {
        public void Configure(EntityTypeBuilder<EntityBase> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
