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
    public class ProjectController(IDataRepository repo) : ControllerBase
    {
        [Route("list")]
        [HttpGet]
        public async Task<IActionResult> GetProjects()
        {
            var projects = await repo.GetAll<ProjectsEntity>().ToListAsync();
            if (projects is null)
            {
                return NotFound();
            }
            var dto = projects.Select(u => new ProjectDTO
            {
                Id = u.Id,
                Title = u.Title,
                Description = u.Description,
                ImageUrl = u.ImageUrl,
                Url = u.Url,
                GithubUrl = u.GithubUrl,
                Tags = u.Tags,
                CreatedAt = DateTime.Now
            }).ToList();

            return Ok(dto);
        }
        [Route("get/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetProject(long id)
        {
            var project = await repo.GetById<ProjectsEntity>(id);
            if (project is null)
            {
                return NotFound();
            }
            var dto = new ProjectDTO
            {
                Id = project.Id,
                Title = project.Title,
                Description = project.Description,
                ImageUrl = project.ImageUrl,
                Url = project.Url,
                GithubUrl = project.GithubUrl,
                Tags = project.Tags,
                CreatedAt = DateTime.Now
            };
            return Ok(dto);
        }
        [Route("add")]
        [HttpPost]
        public async Task<IActionResult> AddProject([FromBody] ProjectDTO projectDTO)
        {
            var project = new ProjectsEntity
            {
                Title = projectDTO.Title,
                Description = projectDTO.Description,
                ImageUrl = projectDTO.ImageUrl,
                Url = projectDTO.Url,
                GithubUrl = projectDTO.GithubUrl,
                Tags = projectDTO.Tags
            };
            await repo.Add(project);
            return Ok(project);
        }
        [Route("edit/{id}")]
        [HttpPut]
        public async Task<IActionResult> UpdateProject([FromBody] ProjectDTO projectDTO,long id)
        {
            var existingProject = await repo.GetById<ProjectsEntity>(id);
            if (existingProject is null)
            {
                return NotFound();
            }
            existingProject.Title = projectDTO.Title;
            existingProject.Description = projectDTO.Description;
            existingProject.ImageUrl = projectDTO.ImageUrl;
            existingProject.Url = projectDTO.Url;
            existingProject.GithubUrl = projectDTO.GithubUrl;
            existingProject.Tags = projectDTO.Tags;

            await repo.Update(existingProject);

            return Ok(existingProject);
        }
        [Route("delete/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteProject(long id)
        {
            var project = await repo.GetById<ProjectsEntity>(id);
            if (project is null)
            {
                return NotFound();
            }
            await repo.Delete(project);
            return Ok();
        }
    }
}
