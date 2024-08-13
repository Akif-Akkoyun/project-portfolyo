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
            List<ExperiencesEntity> experiencesEntities = new()
            {
                new ExperiencesEntity
                {
                    Title = "Software Intern",
                    Company = "Inpol Technology Services ",
                    StartDate = "Jun 2019",
                    EndtDate = "Sep 2019",
                    Description = "This experience is designed to integrate theoretical knowledge with practical experience. This opportunity allowed me to observe the in real-world settings.",
                    CreatedAt = DateTime.Now
                },
                new ExperiencesEntity
                {
                    Title = "IT Support Assistant Specialist",
                    Company = "Tariş",
                    StartDate = "Feb 2020",
                    EndtDate = "Dec 2021",
                    Description = "Technical Assistance: Offer first-line support to users for IT-related issues, including hardware, software, and network problems. Assist in troubleshooting and resolving basic technical issues.",
                    CreatedAt = DateTime.Now
                },
                new ExperiencesEntity
                {
                    Title = "IT Support Specialist",
                    Company = "PerkinElmer",
                    StartDate = "Jan 2022",
                    EndtDate = "Jun 2023",
                    Description = "Customer Service: Deliver high-quality customer service by effectively communicating with users, managing expectations, and following up to ensure issues are resolved to satisfaction. Provide first-line support to end-users via phone, email, and in-person for technical issues related to hardware, software, and network systems.",
                    CreatedAt = DateTime.Now
                },
                new ExperiencesEntity
                {
                    Title = "Career transition",
                    Company = "Career Break",
                    StartDate = "Aug 2023",
                    EndtDate = "Present",
                    Description = "I decided to evolve my career from IT Support to becoming a Software Developer. That's why I've started my training with Siliconmade Academy",
                    CreatedAt = DateTime.Now
                }
            };
            db.Set<ExperiencesEntity>().AddRange(experiencesEntities);
            await db.SaveChangesAsync();
            List<ServiceEntity> services = new()
            {
                new ServiceEntity
                {
                    Name = "C#",
                    CreatedAt = DateTime.Now
                },
                new ServiceEntity
                {
                    Name = "HTML",
                    CreatedAt = DateTime.Now
                },
                new ServiceEntity
                {
                    Name = "JavaScript",
                    CreatedAt = DateTime.Now
                },
                new ServiceEntity
                {
                    Name = "MsOffice",
                    CreatedAt = DateTime.Now
                },
                new ServiceEntity
                {
                    Name = ".Net Mvc",
                    CreatedAt = DateTime.Now
                },
                new ServiceEntity
                {
                    Name = ".Net Api",
                    CreatedAt = DateTime.Now
                },
                new ServiceEntity
                {
                    Name = "Entity Framework Core",
                    CreatedAt = DateTime.Now
                },
                new ServiceEntity
                {
                    Name = "Automapper",
                    CreatedAt = DateTime.Now
                },
                new ServiceEntity
                {
                    Name = "Fluent Validation",
                    CreatedAt = DateTime.Now
                },
                new ServiceEntity
                {
                    Name = "MsSS Management Studio",
                    CreatedAt = DateTime.Now
                },
            };
            db.Set<ServiceEntity>().AddRange(services);
            await db.SaveChangesAsync();
            List<EducationsEntity> educations = new()
            {
                new EducationsEntity
                {
                    Degree = "High School",
                    School = "Ege College Anatolian High School",
                    StartDate = 2012,
                    EndDate = 2016,
                    Description = "I graduated from Kadıköy Anatolian High School in 2015",
                    CreatedAt = DateTime.Now
                }
            };
            db.Set<EducationsEntity>().AddRange(educations);
            await db.SaveChangesAsync();
        }        
    }
}
