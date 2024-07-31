using PortfolyoApp.Admin.Mvc;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace PortfolyoApp.Admin.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
