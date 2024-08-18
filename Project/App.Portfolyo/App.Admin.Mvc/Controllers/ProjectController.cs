using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortfolyoApp.Admin.Mvc.Models;
using PortfolyoApp.Business.DTOs;
using PortfolyoApp.Business.Services;

namespace PortfolyoApp.Admin.Mvc.Controllers
{
    [Authorize]
    public class ProjectController(IUserService service) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> ListProject()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var projectList = await service.ListAsyncProject();

            var model = projectList.Select(u => new ProjectViewModel
            {
                Id = u.Id,
                Title = u.Title,
                Tags = u.Tags,
                GithubUrl = u.GithubUrl,
                Url = u.Url,
                ImageUrl = u.ImageUrl,
                Description = u.Description,
                CreatedAt = DateTime.Now,
            }).ToList();
            return View(model);
        }
        [HttpGet]
        public IActionResult AddProject()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddProject([FromForm] ProjectViewModel projectViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(projectViewModel);
            }

            var dto = new ProjectDTO
            {
                Title = projectViewModel.Title,
                Tags = projectViewModel.Tags,
                GithubUrl = projectViewModel.GithubUrl,
                Url = projectViewModel.Url,
                ImageUrl = projectViewModel.ImageUrl,
                Description = projectViewModel.Description,
                CreatedAt = DateTime.Now,
            };
            var result = await service.AddAsyncProject(dto);

            if (result != null) 
            {
                ViewBag.Success = "Başarı ile eklendi";
            }
            else
            {
                ViewBag.Error = "Ekleme işlemi başarısız oldu";
            }
            return View();
        }
        [HttpGet]
        public IActionResult EditProject()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> EditProject([FromForm] ProjectViewModel projectViewModel,[FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return View(projectViewModel);
            }

            var dto = new ProjectDTO
            {
                Title = projectViewModel.Title,
                Tags = projectViewModel.Tags,
                GithubUrl = projectViewModel.GithubUrl,
                Url = projectViewModel.Url,
                ImageUrl = projectViewModel.ImageUrl,
                Description = projectViewModel.Description,
                CreatedAt = DateTime.Now,
            };
            var result = await service.EditAsyncProject(dto,id);

            if (result != null)
            {
                ViewBag.Success = "Başarı ile güncellendi";
            }
            else
            {
                ViewBag.Error = "Güncelleme işlemi başarısız oldu";
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> DeleteProject([FromRoute] long id)
        {
            var result = await service.DeleteAsyncProject(id);
            return RedirectToAction("ListProject");
        }
    }
}
