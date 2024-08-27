using Microsoft.AspNetCore.Mvc;
using PortfolyoApp.Admin.Mvc.Models;
using AutoMapper;
using PortfolyoApp.Business.DTOs;
using PortfolyoApp.Business.Services;
using Microsoft.AspNetCore.Authorization;

namespace PortfolyoApp.Admin.Mvc.Controllers
{
    [Authorize]
    public class OperationsController(IMapper mapper, IUserService service,IFileService fileService) : Controller
    {
        [HttpGet]
        public IActionResult AboutMeEdit()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AboutMeEdit([FromForm] AboutMeViewModel aboutMeViewModel)
        {
            if (!ModelState.IsValid)
            {
                try
                {
                    long id = 1;
                    var existingProject = await service.AboutDetailAsync(id);
                    if (existingProject == null)
                    {
                        ViewBag.Error = "Blog bulunamadı";
                        return View(aboutMeViewModel);
                    }
                    if (aboutMeViewModel.ImgFile1 != null && aboutMeViewModel.ImgFile1.Length > 0)
                    {
                        if (!string.IsNullOrEmpty(aboutMeViewModel.ImageUrl1))
                        {
                            await fileService.DeleteFileAsync(aboutMeViewModel.ImageUrl1);
                        }

                        var uploadResult1 = await fileService.UploadFileAsync(aboutMeViewModel.ImgFile1);
                        if (uploadResult1.IsSuccess)
                        {
                            aboutMeViewModel.ImageUrl1 = uploadResult1.Value;
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Failed to upload file");
                            return View(aboutMeViewModel);
                        }
                    }

                    var dto = mapper.Map<AboutMeDTO>(aboutMeViewModel);

                    var updateResult = await service.EditAboutMeAsync(dto,id);

                    if (updateResult != null)
                    {
                        ViewBag.Success = "Başarı ile güncellendi";
                    }
                    else
                    {
                        ViewBag.Error = "Güncelleme başarısız oldu";
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Failed to update project: " + ex.Message);
                }
            }

            return View(aboutMeViewModel);
        }
        [HttpGet]
        public async Task<IActionResult> ListExp()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var expList = await service.ListAsyncExp();

            var model = expList.Select(u => new ExperienceViewModel
            {
                Id = u.Id,
                Title = u.Title,
                Company = u.Company,
                EndMonth = u.EndMonth,
                EndtYear = u.EndtYear,
                StartMonth = u.StartMonth,
                StartYear = u.StartYear,
                Description = u.Description,
                CreatedAt = DateTime.Now,
            }).ToList();

            return View(model);
        }
        [HttpGet]
        public IActionResult AddExp()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddExp([FromForm] ExperienceViewModel experienceViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(experienceViewModel);
            }

            var dto = new ExperienceDTO
            {
                Title = experienceViewModel.Title,
                Company = experienceViewModel.Company,
                StartMonth = experienceViewModel.StartMonth,
                StartYear = experienceViewModel.StartYear,
                EndMonth = experienceViewModel.EndMonth,
                EndtYear = experienceViewModel.EndtYear,
                Description = experienceViewModel.Description,
                CreatedAt = DateTime.Now
            };

            var result = await service.AddExpAsync(dto);

            if (result != null)
            {
                ViewBag.Success = "Added successfully";
            }
            else
            {
                ViewBag.Error = "Update failed !";
            }

            return View();
        }
        [HttpGet]
        public IActionResult EditExp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> EditExp([FromForm]ExperienceViewModel experienceViewModel, [FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return View(experienceViewModel);
            }
            var dto = mapper.Map<ExperienceDTO>(experienceViewModel);

            
            var result = await service.EditExpAsync(dto,id);
            if (result != null)
            {
                ViewBag.Success = "Updated successfully";
            }
            else
            {
                ViewBag.Error = "Update failed !";
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> DeleteExp([FromRoute] long id)
        {
            var result = await service.DeleteExpAsync(id);
            
            return RedirectToAction("ListExp");
        }
        [HttpGet]
        public async Task<IActionResult> ListService()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var serviceList = await service.ListAsyncService();

            var model = serviceList.Select(x => new ServiceViewModel
            {
                Id = x.Id,
                Name = x.Name,
                CreatedAt = DateTime.Now
            }).ToList();

            return View(model);
        }
        [HttpGet]
        public IActionResult AddService()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddService([FromForm] ServiceViewModel serviceViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(serviceViewModel);
            }

            var dto = new ServiceDTO
            {
                Id = serviceViewModel.Id,
                Name = serviceViewModel.Name,
                CreatedAt = DateTime.Now
            };

            var result = await service.AddServiceAsync(dto);

            if (result != null)
            {
                ViewBag.Success = "Added successfully";
            }
            else
            {
                ViewBag.Error = "Update failed !";
            }

            return View();
        }
        [HttpGet]
        public IActionResult EditService()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> EditService([FromForm]ServiceViewModel serviceViewModel, [FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return View(serviceViewModel);
            }
            var dto = mapper.Map<ServiceDTO>(serviceViewModel);


            var result = await service.EditServiceAsync(dto, id);
            if (result != null)
            {
                ViewBag.Success = "Updated successfully";
            }
            else
            {
                ViewBag.Error = "Update failed !";
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> DeleteService([FromRoute] long id)
        {
            var result = await service.DeleteServiceAsync(id);

            return RedirectToAction("ListService");
        }
    }
}
