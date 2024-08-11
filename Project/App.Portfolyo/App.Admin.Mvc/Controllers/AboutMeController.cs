using Microsoft.AspNetCore.Mvc;
using PortfolyoApp.Admin.Mvc.Models;
using AutoMapper;
using PortfolyoApp.Business.DTOs;
using PortfolyoApp.Business.Services;

namespace PortfolyoApp.Admin.Mvc.Controllers
{
    public class AboutMeController(IMapper mapper,IUserService service) : Controller
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
    }
}
