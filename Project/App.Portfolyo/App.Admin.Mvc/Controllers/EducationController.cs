using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PortfolyoApp.Admin.Mvc.Models;
using PortfolyoApp.Business.DTOs;
using PortfolyoApp.Business.Services;

namespace PortfolyoApp.Admin.Mvc.Controllers
{
    public class EducationController(IUserService service, IMapper mapper) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> ListEducation()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var educationList = await service.ListAsyncEducation();

            var model = educationList.Select(u => new EducationViewModel
            {
                Id = u.Id,
                Degree = u.Degree,
                School = u.School,
                StartDate = u.StartDate,
                EndDate = u.EndDate,
                Description = u.Description,
                CreatedAt = DateTime.Now,
            }).ToList();

            return View(model);
        }
        [HttpGet]
        public IActionResult AddEducation()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddEducation([FromForm] EducationViewModel educationViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(educationViewModel);
            }

            var dto = new EducationsDTO
            {
                Degree = educationViewModel.Degree,
                School = educationViewModel.School,
                StartDate = educationViewModel.StartDate,
                EndDate = educationViewModel.EndDate,
                Description = educationViewModel.Description,
                CreatedAt = DateTime.Now,
            };
            var result = await service.AddAsyncEducation(dto);

            if (result != null) 
            {
                ViewBag.Success = "Başarı ile eklendi";
            }
            else
            {
                ViewBag.Error = "Ekleme başarısız oldu";
            }
            return View();
        }
        [HttpGet]
        public IActionResult EditEducation()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> EditEducation([FromForm] EducationViewModel educationViewModel, [FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return View(educationViewModel);
            }

            var dto = mapper.Map<EducationsDTO>(educationViewModel);

            var result = await service.EditAsyncEducation(dto, id);

            if (result != null) // Assuming 'result' is null if the update fails
            {
                ViewBag.Success = "Başarı ile güncellendi";
            }
            else
            {
                ViewBag.Error = "Güncelleme başarısız oldu";
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> DeleteEducation([FromRoute] long id)
        {
            var result = await service.DeleteAsyncEducation(id);

            return RedirectToAction("ListEducation");
        }

    }
}
