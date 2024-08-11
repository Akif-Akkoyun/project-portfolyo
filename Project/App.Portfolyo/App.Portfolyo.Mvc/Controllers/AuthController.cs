using Ardalis.Result;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolyoApp.Business.DTOs.Auth;
using PortfolyoApp.Business.Services.Abstract;
using PortfolyoApp.Data.Infrastructure;
using PortfolyoApp.Mvc.Models;
using ServiceStack.Auth;
using ServiceStack.Script;

namespace PortfolyoApp.Mvc.Controllers
{
    [AllowAnonymous]
    public class AuthController(IMapper mapper, IAuthService service) : Controller
    {

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login([FromForm] LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(loginViewModel);
            }
            var dto = mapper.Map<LoginDTO>(loginViewModel);

            
            var result = await service.LoginAsync(dto);

            Response.Cookies.Append("auth-token", result.Value.Token);

            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register([FromForm] RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(registerViewModel);
            }

            var source = mapper.Map<RegisterViewModel, RegisterDTO>(registerViewModel);

            var registerDTO = new RegisterDTO
            {
                UserName = registerViewModel.Name,
                UserSurName = registerViewModel.SurName,
                Email = registerViewModel.Email,
                PasswordHash = registerViewModel.Password
            };
            var result = await service.RegistersAsync(registerDTO);

            return RedirectToAction("Login", "Auth");
        }
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgotPassword([FromForm] ForgotPasswordViewModel forgotPasswordViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(forgotPasswordViewModel);
            }

            var source = mapper.Map<ForgotPasswordViewModel, ForgotPasswordDTO>(forgotPasswordViewModel);

            var forgotPasswordDTO = new ForgotPasswordDTO
            {
                Email = forgotPasswordViewModel.Email
            };

            var result = await service.ForgotPasswordAsync(forgotPasswordDTO);

            return RedirectToAction(nameof(RenewPassword));
        }
        [HttpGet]
        public IActionResult RenewPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> RenewPassword([FromForm] RenewPasswordViewModel reNewPasswordViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(reNewPasswordViewModel);
            }

            var source = mapper.Map<RenewPasswordViewModel, ResetPasswordDTO>(reNewPasswordViewModel);

            var resetPasswordDTO = new ResetPasswordDTO
            {
                Token = reNewPasswordViewModel.Token,
                Email = reNewPasswordViewModel.Email,
                PasswordHash = reNewPasswordViewModel.Password

            };

            var result = await service.RenewPasswordAsync(resetPasswordDTO);

            if(!result.IsSuccess)
            {
                ModelState.AddModelError(string.Empty, "Invalid code or email");
            }

            return RedirectToAction("Index","Home");
        }
    }
}
