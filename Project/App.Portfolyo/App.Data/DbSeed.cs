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
                    Day = 1,
                    Month = "January",
                    Year = 1997,
                    Address = "Istanbul Kadıköy",
                    Email = "akifakkoyun09@gmail.com",
                    PhoneNumber = "0532 123 45 67",
                    CreatedAt = DateTime.Now,
                    ZipCode = 34722
                }
            };
            db.Set<AboutMeEntity>().AddRange(aboutMeEntities);
            await db.SaveChangesAsync();
            List<SliderEntity> sliderEntity = new()
            {
                new SliderEntity
                {
                    ImgUrl1 = "slider1.jpg",
                    ImgUrl2 = "slider2.jpg",
                }
            };
            db.Set<SliderEntity>().AddRange(sliderEntity);
            await db.SaveChangesAsync();
            List<ExperiencesEntity> experiencesEntities = new()
            {
                new ExperiencesEntity
                {
                    Title = "Software Intern",
                    Company = "Inpol Technology Services ",
                    StartMonth = "Jul",
                    StartYear = 2019,
                    EndMonth = "Sep",
                    EndtYear = 2019,
                    Description = "This experience is designed to integrate theoretical knowledge with practical experience. This opportunity allowed me to observe the in real-world settings.",
                    CreatedAt = DateTime.Now
                },
                new ExperiencesEntity
                {
                    Title = "IT Support Assistant Specialist",
                    Company = "Tariş",
                    StartMonth = "Feb ",
                    StartYear = 2020,
                    EndMonth = "Dec",
                    EndtYear = 2021,
                    Description = "Technical Assistance: Offer first-line support to users for IT-related issues, including hardware, software, and network problems. Assist in troubleshooting and resolving basic technical issues.",
                    CreatedAt = DateTime.Now
                },
                new ExperiencesEntity
                {
                    Title = "IT Support Specialist",
                    Company = "PerkinElmer",
                    StartMonth = "Jan",
                    StartYear = 2022,
                    EndMonth = "Jun",
                    EndtYear = 2023,
                    Description = "Customer Service: Deliver high-quality customer service by effectively communicating with users, managing expectations, and following up to ensure issues are resolved to satisfaction. Provide first-line support to end-users via phone, email, and in-person for technical issues related to hardware, software, and network systems.",
                    CreatedAt = DateTime.Now
                },
                new ExperiencesEntity
                {
                    Title = "Career transition",
                    Company = "Career Break",
                    StartMonth = "Augt",
                    StartYear = 2023,
                    EndMonth = "Present",
                    EndtYear = 2024,
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
            List<ProjectsEntity> projects = new()
            {
                new ProjectsEntity
                {
                    Title = "E-Ticaret Site",
                    Description = "This is a project that I developed with .Net Core Mvc. It is a personal website project. It is a project that I developed with .Net Core Mvc. It is a personal website project.",
                    ImageUrl = "eticaret.png",
                    Url = "https://github.com/Akif-Akkoyun",
                    Tags = "Web Development",
                    CreatedAt = DateTime.Now,
                    GithubUrl = "https://github.com/Akif-Akkoyun",
                },
                new ProjectsEntity
                {
                    Title = "Login Page",
                    Description = "This is a project that I developed with .Net Core Mvc. It is a personal website project. It is a project that I developed with .Net Core Mvc. It is a personal website project.",
                    ImageUrl = "message.png",
                    Url = "https://github.com/Akif-Akkoyun",
                    Tags = "Web Development",
                    CreatedAt = DateTime.Now,
                    GithubUrl = "https://github.com/Akif-Akkoyun",
                },
                new ProjectsEntity
                {
                    Title = "Portfolyo Page",
                    Description = "This is a project that I developed with .Net Core Mvc. It is a personal website project. It is a project that I developed with .Net Core Mvc. It is a personal website project.",
                    ImageUrl = "portfolyo.png",
                    Url = "https://github.com/Akif-Akkoyun",
                    Tags = "Web Development",
                    CreatedAt = DateTime.Now,
                    GithubUrl = "https://github.com/Akif-Akkoyun",
                }
            };
            db.Set<ProjectsEntity>().AddRange(projects);
            await db.SaveChangesAsync();
            List<BlogPostEntity> blogPosts = new()
            {
                new BlogPostEntity
                {
                    Title = "How to create a blog site with .Net Core Mvc",
                    Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec gravida vehicula urna, vitae cursus erat maximus ac. Ut ut elementum tortor. Proin at massa venenatis, ullamcorper quam ac, hendrerit justo. Proin et velit posuere, condimentum elit sit amet, vestibulum felis. Suspendisse pharetra lectus vitae accumsan venenatis. Aliquam euismod tellus non venenatis tempor. Donec sed libero malesuada, dignissim lectus eu, tincidunt lacus. Praesent vitae sapien a quam pretium accumsan quis nec ex. Donec lobortis mauris at dignissim elementum. Pellentesque nec magna ut ex posuere blandit. Duis tempor auctor finibus. Nulla iaculis vestibulum semper. Vivamus id urna quam. Quisque augue neque, pretium nec augue non, iaculis condimentum elit.\r\n\r\nAliquam interdum blandit tortor, sed eleifend sapien laoreet ut. Ut vitae hendrerit mauris. Fusce finibus bibendum auctor. Nunc rutrum commodo lobortis. Phasellus id mi id eros blandit consequat ac ut mi. Vivamus volutpat dolor eu velit lacinia ullamcorper. Sed pretium euismod ex, faucibus maximus ligula sagittis eget. Morbi vulputate rutrum nibh, at ornare dui accumsan et. Ut dictum purus metus, vitae mollis lacus finibus viverra.\r\n\r\nEtiam viverra erat sed vestibulum porta. Nunc ornare ac lorem id volutpat. Mauris felis lacus, vestibulum eu tortor sit amet, egestas fermentum justo. Proin suscipit mollis accumsan. Aliquam quis dui vitae massa luctus finibus. Ut efficitur, lacus id tristique venenatis, nulla elit tincidunt nisi, sed maximus nibh arcu pharetra nulla. Cras aliquet semper felis vel laoreet. Sed nunc nisi, varius gravida nibh a, ultricies sollicitudin purus. Nullam posuere, purus non aliquam laoreet, elit arcu dignissim lacus, nec convallis nulla tortor id purus.\r\n\rMauris fringilla, nisi eget viverra pulvinar, diam velit rhoncus augue, eu rhoncus est dui sit amet orci. Vestibulum in interdum nisi. Morbi libero orci, rhoncus vel iaculis sed, eleifend eget odio. Pellentesque condimentum consequat odio a malesuada. Nullam id ante ac erat posuere fringilla sed at risus. Sed eu auctor purus. Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Sed viverra sollicitudin magna, a vestibulum nisi cursus vehicula. Curabitur non erat et dui pellentesque scelerisque sit amet id leo. Phasellus rhoncus tincidunt tortor, eget aliquet quam pulvinar at. Nulla elementum dictum orci non varius. Fusce eget rutrum risus. Pellentesque dictum accumsan ex, quis malesuada magna scelerisque porttitor. Etiam fermentum massa sit amet neque ullamcorper auctor. Cras efficitur elit sit amet justo tincidunt, id consectetur dolor pretium.\r\n\r\nPhasellus et ipsum condimentum sapien luctus posuere. Morbi lorem mi, vehicula ac pulvinar imperdiet, cursus ut augue. Vestibulum mattis in risus in placerat. Sed mattis, dui in eleifend hendrerit, orci dolor sagittis odio, a mattis arcu lectus id magna. Etiam gravida ante nibh, sit amet vehicula nibh rutrum vel. Suspendisse in ultrices magna. Phasellus malesuada est turpis, a feugiat risus blandit vel. Ut sodales, massa vel semper bibendum, justo nunc fermentum diam, eget scelerisque lacus augue nec orci. Nam ultrices erat commodo dictum efficitur. Ut gravida vestibulum vulputate. Integer sed condimentum erat, vitae aliquam sapien. Nam blandit turpis at sem scelerisque semper.\r\n\r\nNunc porttitor eleifend porta. Interdum et malesuada fames ac ante ipsum primis in faucibus. Vestibulum vel nulla massa. Nullam tortor lacus, imperdiet sed porttitor sed, tincidunt fringilla urna. Praesent tincidunt nunc non varius pretium. Pellentesque facilisis vehicula nulla, eu tincidunt eros lobortis id. Curabitur nec velit nec urna bibendum euismod ut sed neque. Pellentesque posuere, metus et rutrum ornare, nisi risus suscipit turpis, id venenatis massa magna nec augue.\r\n\r\nPhasellus nisl lectus, porta ut turpis pulvinar, aliquet dapibus quam. Etiam convallis viverra felis, eu auctor dui convallis pulvinar. Duis ac cursus enim. Nulla at sem tempor, sagittis orci a, luctus sem. Mauris fringilla lacinia dolor, sit amet rutrum justo hendrerit ac. Nulla nec molestie sapien, et vestibulum massa. Cras malesuada viverra turpis, sit amet semper justo sagittis a. Mauris a porttitor ipsum. Aliquam suscipit felis at erat pulvinar porta.\r\n\r\nPhasellus efficitur ante ipsum, et gravida nunc suscipit at. Aenean malesuada id tortor at viverra. Curabitur eget maximus eros. Integer malesuada, ex non fermentum vulputate, leo ex sollicitudin nisl, vitae sagittis enim nisl sit amet justo. Quisque ut neque ac diam fringilla sagittis. Proin tempor imperdiet hendrerit. Phasellus et dictum turpis, vestibulum lacinia odio. Vivamus hendrerit erat accumsan, vestibulum ligula in, fringilla neque.",
                    ImageUrl = "/theme/images/project-2.jpg",
                    PublishDate = DateTime.Now,
                    CreatedAt = DateTime.Now
                },
                new BlogPostEntity
                {
                    Title = "How to create a blog site with .Net Core Mvc",
                    Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc finibus in lacus quis malesuada. Praesent commodo nulla arcu, nec dictum enim elementum a. Donec elementum nibh non dui vestibulum, sed fringilla nisl malesuada. Nam ac egestas tortor. Sed orci mauris, feugiat sed ultrices vel, efficitur nec magna. In eget orci fringilla, interdum turpis in, porttitor odio. Aenean congue congue ipsum vitae tincidunt. Suspendisse consequat non tortor ac scelerisque.\r\n\r\nPellentesque ac sem tempus, bibendum tellus vel, sodales est. In finibus lacus a mauris dictum vestibulum. Vivamus sagittis nisl porttitor dignissim sagittis. Vivamus pharetra mollis venenatis. Proin consequat molestie nisi, nec feugiat justo interdum ac. Quisque cursus aliquet nisi et consequat. Praesent ut volutpat metus. Morbi vehicula elementum quam eget dictum. Nullam ullamcorper mollis nisl. Nulla facilisi. Phasellus est elit, condimentum non luctus at, fringilla venenatis mi. Fusce tincidunt id lectus eget egestas. Quisque pulvinar iaculis feugiat.\r\n\r\nEtiam at sem efficitur, ultricies ipsum in, vulputate magna. Curabitur faucibus malesuada arcu eu tristique. Nulla a odio sapien. Mauris luctus semper viverra. In eget justo a purus mattis accumsan. In hac habitasse platea dictumst. Nullam iaculis, turpis non vehicula commodo, diam dui blandit neque, vitae aliquet ligula nisi in mauris. Nam ante nisi, placerat nec egestas vel, rutrum sit amet risus. Proin congue lacus sit amet tellus hendrerit, in feugiat urna lobortis. Ut condimentum purus non ante cursus, ac pulvinar sem eleifend. Mauris maximus quis turpis ac volutpat. Suspendisse sagittis pulvinar odio, id gravida purus mattis imperdiet. Nulla facilisi. Integer lacinia nec nibh sodales iaculis. Maecenas pretium consequat fringilla.\r\n\r\nPraesent nec eleifend erat. Vestibulum in commodo lorem. Mauris ut risus luctus, aliquet felis sit amet, hendrerit risus. Integer eu leo eget turpis vulputate porta. Sed porttitor gravida ante a vehicula. Nulla condimentum lacus turpis, quis rhoncus odio aliquet eu. Morbi id elit mauris. Nunc imperdiet massa ut magna ultricies, sed efficitur sem gravida. Nulla non ultrices risus, id iaculis ex. Nunc interdum leo sed dolor vehicula rutrum. Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Fusce blandit nisi lacus, eu eleifend odio vestibulum id. Curabitur ac urna dui. Aenean libero sem, lacinia sit amet nisi et, lobortis lacinia orci. Nunc metus ante, sagittis quis erat non, tincidunt dignissim felis. Aliquam feugiat, magna iaculis volutpat tempus, neque felis ornare nisl, ut dictum odio felis viverra magna.\r\n\r\nDonec pharetra mauris vitae ex fringilla congue. Aenean sagittis ligula erat, eget posuere mauris gravida id. In auctor mi a mi tincidunt ultricies. In hac habitasse platea dictumst. Duis metus ipsum, malesuada id pulvinar vel, consectetur quis est. Aenean lacinia viverra ultrices. Phasellus efficitur ultrices metus. Mauris in ornare ipsum. Nunc sed venenatis mi, in semper nunc. Vestibulum venenatis odio nec metus tincidunt, et mattis purus condimentum. Vestibulum non nibh felis. Fusce faucibus tincidunt ornare.",
                    ImageUrl = "/theme/images/project-3.jpg",
                    PublishDate = DateTime.Now,
                    CreatedAt = DateTime.Now
                },
                new BlogPostEntity
                {
                    Title = "How to create a blog site with .Net Core Mvc",
                    Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam vitae ligula eu lectus sollicitudin interdum. Aliquam euismod odio ac elit aliquam posuere cursus vitae lorem. Vivamus efficitur pretium justo, sed ornare dui tristique bibendum. Sed ipsum eros, cursus vitae velit quis, tempus ultrices dui. Curabitur at hendrerit eros. In vitae urna vel enim vehicula tincidunt eget eget augue. Ut pellentesque enim at blandit facilisis. Sed molestie tempor turpis, tempor tempor urna porta eget. Donec ut nibh sed diam viverra venenatis sed vel tortor. Maecenas imperdiet turpis a hendrerit iaculis.\r\n\r\nEtiam quis ante vitae lorem sodales bibendum. Ut magna nibh, aliquam lobortis accumsan a, tincidunt in tellus. Curabitur sed sapien maximus diam tincidunt tempus. Integer at ligula fringilla, commodo sem at, iaculis dui. Donec commodo sem lectus, eu euismod libero faucibus vitae. Nunc lacinia enim lorem, id interdum erat hendrerit dignissim. Cras non augue elit. Proin pellentesque ut sapien sit amet blandit. Suspendisse imperdiet dignissim magna vitae placerat. Vestibulum posuere nunc id eros ultricies cursus. Nulla in porttitor libero.\r\n\r\nMorbi ac tempor ex, a hendrerit tortor. Etiam ac nisl quis nulla dapibus vestibulum. Morbi at molestie mi. Curabitur at fermentum leo. Fusce viverra sollicitudin ex. Praesent in commodo tortor, congue sagittis erat. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia curae; Vestibulum mattis nec tellus eu condimentum. Pellentesque porttitor, enim vel pulvinar pharetra, mauris augue condimentum lorem, vel venenatis quam dolor id nisi. Quisque imperdiet id odio ut laoreet. Aenean non magna in quam sollicitudin venenatis sit amet ut odio. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Curabitur eu velit ut sem pharetra pulvinar.\r\n\r\nEtiam lacinia id dui id accumsan. Suspendisse odio magna, scelerisque in lacinia sit amet, sollicitudin at neque. Etiam vitae suscipit sapien, nec placerat velit. Donec vitae sem tincidunt, porttitor quam id, molestie turpis. Integer fringilla diam sed sodales ornare. Cras vel quam augue. Nulla eget leo quis magna volutpat fermentum quis nec lacus. Etiam nisi eros, tincidunt vel convallis vitae, mollis ac urna. Suspendisse rhoncus sagittis enim. Duis quis quam erat. In sed arcu eros. Fusce sit amet tellus urna. Duis ut pharetra elit, non dignissim libero. Aenean iaculis risus vel varius ultricies.\r\n\r\nNam nec orci magna. Aenean dapibus quam non porta sollicitudin. Suspendisse mauris urna, cursus et mollis id, suscipit nec purus. Pellentesque porta sapien a dolor sodales, nec congue ipsum porttitor. Cras risus neque, cursus ac urna id, viverra ullamcorper metus. Duis pellentesque massa ut pharetra convallis. Pellentesque augue dui, posuere eget purus non, elementum fringilla erat. Vivamus eget tempus augue. Ut vel vehicula lacus. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia curae; Pellentesque a fringilla leo, vel gravida velit. Vestibulum pretium at ligula vel malesuada. Aliquam pulvinar dolor ante.",
                    ImageUrl = "/theme/images/project-4.jpg",
                    PublishDate = DateTime.Now,
                    CreatedAt = DateTime.Now
                }
            };
            db.Set<BlogPostEntity>().AddRange(blogPosts);
            await db.SaveChangesAsync();
        }        
    }
}
