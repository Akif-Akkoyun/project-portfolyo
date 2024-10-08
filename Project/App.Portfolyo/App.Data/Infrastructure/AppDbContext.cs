﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using PortfolyoApp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolyoApp.Data.Infrastructure
{
    internal class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<AboutMeEntity> Abouts { get; set; } = null!;
        public DbSet<ExperiencesEntity> Experiences { get; set; } = null!;
        public DbSet<ProjectsEntity> Projects { get; set; } = null!;
        public DbSet<ContactMessagesEntity> ContactMassages { get; set; } = null!;
        public DbSet<EducationsEntity> Educations { get; set; } = null!;
        public DbSet<ServiceEntity> Skılls { get; set; } = null!;
        public DbSet<BlogPostEntity> BlogPosts { get; set; } = null!;
        public DbSet<FileEntity> Files { get; set; } = null!;
        public DbSet<SliderEntity> Sliders { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AboutMeConfiguration());
            modelBuilder.ApplyConfiguration(new FileConfiguration());
            modelBuilder.ApplyConfiguration(new BlogPostConfiguration());
            modelBuilder.ApplyConfiguration(new ExperiencesConfiguration());
            modelBuilder.ApplyConfiguration(new ProjectsConfiguration());
            modelBuilder.ApplyConfiguration(new ContactMessagesConfiguration());
            modelBuilder.ApplyConfiguration(new EducationsConfiguration());
            modelBuilder.ApplyConfiguration(new ServiceEntityConfiguration());
            modelBuilder.ApplyConfiguration(new SliderConfiguration());
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DbContext).Assembly);
        }
    }
}
