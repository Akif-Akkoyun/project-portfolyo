﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PortfolyoApp.Data.Api.Entities
{
    public class Experiences : EntityBase
    {
        public string Title { get; set; } = default!;
        public string Company { get; set; } = default!;
        public DateTime StartDate { get; set; }
        public DateTime EndtDate { get; set; }
        public string Description { get; set; } = default!;
    }
    internal class ExperiencesConfiguration : IEntityTypeConfiguration<Experiences>
    {
        public void Configure(EntityTypeBuilder<Experiences> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Title).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Company).IsRequired().HasMaxLength(100);
            builder.Property(x => x.StartDate).IsRequired();
            builder.Property(x => x.EndtDate).IsRequired();
            builder.Property(x => x.Description).IsRequired().HasMaxLength(255);
        }
    }
}
