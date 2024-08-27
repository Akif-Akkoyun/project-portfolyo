using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolyoApp.Business.DTOs;
using PortfolyoApp.Data.Entities;
using PortfolyoApp.Data.Infrastructure;

namespace PortfolyoApp.Data.Api.Controllers
{
    [Route("api/v2/[controller]")]
    [ApiController]
    public class AboutMeController(IDataRepository repo) : ControllerBase
    {
        [Route("edit/{id}")]
        [HttpPut]
        public async Task<IActionResult> EditAboutMe(AboutMeDTO aboutMeDTO,long id)
        {
            var abouts = await repo.GetById<AboutMeEntity>(id);

            if (abouts is null)
            {
                return NotFound();
            }
            abouts.ImageUrl1 = aboutMeDTO.ImageUrl1;
            abouts.Introduction = aboutMeDTO.Introduction;
            abouts.Name = aboutMeDTO.Name;
            abouts.PhoneNumber = aboutMeDTO.PhoneNumber;
            abouts.Email = aboutMeDTO.Email;
            abouts.Address = aboutMeDTO.Address;
            abouts.Day = aboutMeDTO.Day;
            abouts.Month = aboutMeDTO.Month;
            abouts.Year = aboutMeDTO.Year;
            abouts.ZipCode = aboutMeDTO.ZipCode;


            await repo.Update(abouts);

            return Ok(abouts);
        }
        [Route("detail/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetAbout(long id)
        {
            var about = await repo.GetById<AboutMeEntity>(id);

            if (about is null)
            {
                return NotFound();
            }

            var dto = new AboutMeDTO
            {
                Id = about.Id,
                ImageUrl1 = about.ImageUrl1,
                Introduction = about.Introduction,
                Name = about.Name,
                PhoneNumber = about.PhoneNumber,
                Email = about.Email,
                Address = about.Address,
                Day = about.Day,
                Month = about.Month,
                Year = about.Year,
                CreatedAt = DateTime.Now,
                ZipCode = about.ZipCode
            };

            return Ok(dto);
        }
        [HttpGet("list")]
        public async Task<IActionResult> GetAbout()
        {
            var abouts = await repo.GetAll<AboutMeEntity>().ToListAsync();

            if(abouts is null)
            {
                return NotFound();
            }

            var dto = abouts.Select(u => new AboutMeDTO
            {
                Id = u.Id,
                ImageUrl1 = u.ImageUrl1,
                Introduction = u.Introduction,
                Name = u.Name,
                PhoneNumber = u.PhoneNumber,
                Email = u.Email,
                Address = u.Address,
                Day = u.Day,
                Month = u.Month,
                Year = u.Year,
                CreatedAt = DateTime.Now,
                ZipCode = u.ZipCode
            }).ToList();

            return Ok(dto);
        }
    }
}
