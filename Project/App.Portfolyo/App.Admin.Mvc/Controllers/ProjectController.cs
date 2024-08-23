using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PortfolyoApp.Admin.Mvc.Models;
using PortfolyoApp.Business.DTOs;
using PortfolyoApp.Business.Services;
using PortfolyoApp.Business.Services.Abstract;

namespace PortfolyoApp.Admin.Mvc.Controllers
{
    [Authorize]
    public class ProjectController(IUserService service,FileService fileService,IMapper mapper) : Controller
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
        public async Task<IActionResult> AddProject(ProjectViewModel projectViewModel)
        {
            if (ModelState.IsValid)
            {
                if (projectViewModel.ImageFile != null && projectViewModel.ImageFile.Length > 0)
                {
                    var result = await fileService.UploadFileAsync(projectViewModel.ImageFile);

                    if (result.IsSuccess)
                    {
                        var filePath = result.Value;

                        // Continue processing the project with the file path
                        var projectDto = mapper.Map<ProjectDTO>(projectViewModel);
                        projectDto.ImageUrl = filePath;

                        try
                        {
                            await service.AddAsyncProject(projectDto);
                            return View();
                        }
                        catch (Exception ex)
                        {
                            ModelState.AddModelError(string.Empty, "Failed to add project: " + ex.Message);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty , "Failed to add project" );
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Please select a file to upload.");
                }
            }

            return View(projectViewModel);
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
