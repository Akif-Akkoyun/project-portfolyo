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
        public IActionResult Index()
        {
            return View();
        }
    }
}
