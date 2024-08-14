using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolyoApp.Business.DTOs;
using PortfolyoApp.Business.Services;
using PortfolyoApp.Data.Entities;
using PortfolyoApp.Data.Infrastructure;

namespace PortfolyoApp.Data.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExperienceController(IDataRepository repo) : ControllerBase
    {
        [HttpGet("list")]
        public async Task<IActionResult> GetExperience()
        {
            var experiences = await repo.GetAll<ExperiencesEntity>().ToListAsync();

            if(experiences is null)
            {
                return NotFound();
            }

            var dto = experiences.Select(u => new ExperienceDTO
            {
                Id = u.Id,
                Title = u.Title,
                Company = u.Company,
                StartMonth = u.StartMonth,
                StartYear = u.StartYear,
                EndMonth = u.EndMonth,
                EndtYear = u.EndtYear,
                Description = u.Description,
                CreatedAt = DateTime.Now
            }).ToList().OrderByDescending(x => x.Id);

            return Ok(dto);
        }
        [Route("add")]
        [HttpPost]
        public async Task<IActionResult> AddExperience(ExperienceDTO experienceDTO)
        {
            var experience = new ExperiencesEntity
            {
                Title = experienceDTO.Title,
                Company = experienceDTO.Company,
                StartMonth = experienceDTO.StartMonth,
                StartYear = experienceDTO.StartYear,
                EndMonth = experienceDTO.EndMonth,
                EndtYear = experienceDTO.EndtYear,
                Description = experienceDTO.Description,
                CreatedAt = DateTime.Now
            };

            await repo.Add(experience);

            return Ok(experience);
        }

        [Route("edit/{id}")]
        [HttpPost]
        public async Task<IActionResult> EditExperience(ExperienceDTO experienceDTO,long id)
        {
            var experiences = await repo.GetById<ExperiencesEntity>(id);

            if(experiences is null)
            {
                return NotFound();
            }
            
            experiences.Title = experienceDTO.Title;
            experiences.Company = experienceDTO.Company;
            experiences.StartMonth = experienceDTO.StartMonth;
            experiences.StartYear = experienceDTO.StartYear;
            experiences.EndMonth = experienceDTO.EndMonth;
            experiences.EndtYear = experienceDTO.EndtYear;
            experiences.Description = experienceDTO.Description;

            await repo.Update(experiences);

            return Ok(experiences);
        }
        [Route("delete/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteExperience(long id)
        {
            var experience = await repo.GetById<ExperiencesEntity>(id);
            if (experience is null)
            {
                return NotFound();
            }
            await repo.Delete(experience);
            return Ok();
        }
    }
}
