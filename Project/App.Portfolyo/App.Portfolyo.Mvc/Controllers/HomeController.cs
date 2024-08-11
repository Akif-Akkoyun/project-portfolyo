using Microsoft.AspNetCore.Mvc;
using PortfolyoApp.Business.Services;
using PortfolyoApp.Mvc.Models;


namespace PortfolyoApp.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserService _service;

        public HomeController(ILogger<HomeController> logger,IUserService service)
        {
            _logger = logger;
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var listAbout = await _service.GetList();

            if (listAbout is null)
            {
                return NotFound();
            }
            var result = listAbout.Select(u => new AboutMeViewModel
            {
                Id = u.Id,
                ImageUrl1 = u.ImageUrl1,
                Introduction = u.Introduction,
                Name = u.Name,
                PhoneNumber = u.PhoneNumber,
                Email = u.Email,
                Address = u.Address,
                DateOfbirth = u.DateOfbirth,
                CreatedAt = u.CreatedAt,
                ZipCode = u.ZipCode
            }).ToList();

            return View(result);
        }
    }
}
