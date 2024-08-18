using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolyoApp.Business.DTOs;
using PortfolyoApp.Data.Entities;
using PortfolyoApp.Data.Infrastructure;

namespace PortfolyoApp.Data.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EducationsController(IDataRepository repo) : ControllerBase
    {
        [HttpGet("list")]
        public async Task<IActionResult> GetEducations()
        {
            var educations = await repo.GetAll<EducationsEntity>().ToListAsync();
            if (educations is null)
            {
                return NotFound();
            }
            var dto = educations.Select(u => new EducationsDTO
            {
                Id = u.Id,
                Degree = u.Degree,
                School = u.School,
                StartDate = u.StartDate,
                EndDate = u.EndDate,
                Description = u.Description
            }).ToList().OrderByDescending(u=>u.Id);

            return Ok(dto);
        }
        [Route("add")]
        [HttpPost]
        public async Task<IActionResult> AddEducation(EducationsDTO educationDTO)
        {
            var education = new EducationsEntity
            {
                Degree = educationDTO.Degree,
                School = educationDTO.School,
                StartDate = educationDTO.StartDate,
                EndDate = educationDTO.EndDate,
                Description = educationDTO.Description
            };

            await repo.Add(education);

            return Ok(education);
        }
        [Route("edit/{id}")]
        [HttpPost]
        public async Task<IActionResult> EditEducation(EducationsDTO educationDTO, long id)
        {
            var education = await repo.GetById<EducationsEntity>(id);
            if (education is null)
            {
                return NotFound();
            }
            education.Degree = educationDTO.Degree;
            education.School = educationDTO.School;
            education.StartDate = educationDTO.StartDate;
            education.EndDate = educationDTO.EndDate;
            education.Description = educationDTO.Description;

            await repo.Update(education);

            return Ok(education);
        }
        [Route("delete/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteEducation(long id)
        {
            var education = await repo.GetById<EducationsEntity>(id);
            if (education is null)
            {
                return NotFound();
            }

            await repo.Delete(education);

            return Ok();
        }
    }
}
