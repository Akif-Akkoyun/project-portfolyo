using PortfolyoApp.Admin.Mvc;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using PortfolyoApp.Business.Services.Abstract;
using PortfolyoApp.Admin.Mvc.Models;
using PortfolyoApp.Business.Services;
using AutoMapper;
using PortfolyoApp.Business.DTOs;
using Microsoft.AspNetCore.Authorization;
using PortfolyoApp.Data.Infrastructure;

namespace PortfolyoApp.Admin.Mvc.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserService _service;

        public HomeController(ILogger<HomeController> logger, IUserService service)
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
