using Microsoft.AspNetCore.Mvc;
using PortfolyoApp.Admin.Mvc.Models;
using AutoMapper;
using PortfolyoApp.Business.DTOs;
using PortfolyoApp.Business.Services;
using System.Linq;
using ServiceStack;

namespace PortfolyoApp.Admin.Mvc.Controllers
{
    public class OperationsController(IMapper mapper, IUserService service) : Controller
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
                return View(aboutMeViewModel);
            }

            var result = await service.UpdateAsync(mapper.Map<AboutMeDTO>(aboutMeViewModel));

            if (result != null) // Assuming 'result' is null if the update fails
            {
                ViewBag.Success = "Başarı ile güncellenmiştir";
            }
            else
            {
                ViewBag.Error = "Güncelleme başarısız oldu";
            }
            return View();
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
                StartDate = u.StartDate,
                EndtDate = u.EndtDate,
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
                StartDate = experienceViewModel.StartDate,
                EndtDate = experienceViewModel.EndtDate,
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
        public async Task<IActionResult> EditExp(ExperienceViewModel experienceViewModel, [FromRoute] long id)
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
    }
}
