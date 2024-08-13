using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolyoApp.Data.Entities
{
    public class ServiceEntity : EntityBase
    {
        public string Name { get; set; } = default!;
    }
    internal class ServiceEntityConfiguration : IEntityTypeConfiguration<ServiceEntity>
    {
        public void Configure (EntityTypeBuilder<ServiceEntity> builder)
        {
            builder.Property(x=> x.Name).IsRequired().HasMaxLength(100);
        }
    }
}
