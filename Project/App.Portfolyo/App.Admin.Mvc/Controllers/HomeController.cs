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
using ServiceStack;

namespace PortfolyoApp.Admin.Mvc.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAuthService _service;

        public HomeController(ILogger<HomeController> logger, IAuthService service)
        {
            _logger = logger;
            _service = service;
        }
       
        public IActionResult Index()
        {
            return View();
        }
    }
}
