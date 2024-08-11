using Microsoft.EntityFrameworkCore;
using PortfolyoApp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolyoApp.Data
{
    public class DbSeed
    {
        public static async Task SeedData(DbContext db)
        {
            List<AboutMeEntity> aboutMeEntities = new()
            {
                new AboutMeEntity
                {
                    Introduction = "I am a software developer",
                    ImageUrl1 = "~theme/photo/akif.jpg",
                    Name = "Akif",
                    DateOfbirth = "January 01, 1997",
                    Address = "Istanbul Kadıköy",
                    Email = "akifakkoyun09@gmail.com",
                    PhoneNumber = "0532 123 45 67",
                    CreatedAt = DateTime.Now,
                    ZipCode = 34722
                }
            };
            db.Set<AboutMeEntity>().AddRange(aboutMeEntities);
            await db.SaveChangesAsync();
        }        
    }
}
