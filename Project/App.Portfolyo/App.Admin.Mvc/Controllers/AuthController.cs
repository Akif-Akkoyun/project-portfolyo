using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using AutoMapper;
using PortfolyoApp.Business.DTOs.Auth;
using ServiceStack;
using PortfolyoApp.Admin.Mvc.Models;
using PortfolyoApp.Business.Services.Abstract;
using PortfolyoApp.Auth.Api.Data.Entites;

namespace PortfolyoApp.Admin.Mvc.Controllers
{
    public class AuthController(IMapper mapper,IAuthService service) : Controller
    {
        [HttpPost]
        public IActionResult Login()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Login([FromForm] LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(loginViewModel);
            }
            var source = mapper.Map<LoginViewModel, LoginDTO>(loginViewModel);

            var loginDTO = new LoginDTO
            {
                Email = loginViewModel.Email,
                PasswordHash = loginViewModel.Password
            };
            var result = await service.LoginAsync(loginDTO);

            return RedirectToAction("Index", "Home");
        }
        public IActionResult Logout()
        {
            Logout();
            return RedirectToAction("Login", "Auth");
        }
    }        
}
