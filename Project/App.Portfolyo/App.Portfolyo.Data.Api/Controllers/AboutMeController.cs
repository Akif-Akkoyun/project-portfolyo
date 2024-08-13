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
        [Route("editaboutme")]
        [HttpPost]
        public async Task<IActionResult> EditAboutMe(AboutMeDTO aboutMeDTO)
        {
            var abouts = await repo.GetAll<AboutMeEntity>().ToListAsync();

            foreach (var about in abouts)
            {
                about.ImageUrl1 = aboutMeDTO.ImageUrl1;
                about.Introduction = aboutMeDTO.Introduction;
                about.Name = aboutMeDTO.Name;
                about.PhoneNumber = aboutMeDTO.PhoneNumber;
                about.Email = aboutMeDTO.Email;
                about.Address = aboutMeDTO.Address;
                about.DateOfbirth = aboutMeDTO.DateOfbirth;
                about.ZipCode = aboutMeDTO.ZipCode;

                await repo.Update(about);
            }

            return Ok(abouts);
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
                DateOfbirth = u.DateOfbirth,
                CreatedAt = DateTime.Now,
                ZipCode = u.ZipCode
            }).ToList();

            return Ok(dto);
        }
    }
}
