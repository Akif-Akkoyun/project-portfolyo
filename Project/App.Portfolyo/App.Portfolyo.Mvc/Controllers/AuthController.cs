using Ardalis.Result;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolyoApp.Business.DTOs.Auth;
using PortfolyoApp.Business.Services.Abstract;
using PortfolyoApp.Data.Entities;
using PortfolyoApp.Data.Infrastructure;
using PortfolyoApp.Mvc.Models;
using ServiceStack.Auth;
using ServiceStack.Script;
using System.Security.Claims;

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
            var dto = new LoginDTO
            {
                Email = loginViewModel.Email,
                PasswordHash = loginViewModel.Password
            };
            var result = await service.UserLoginAsync(dto);
            if (!result.IsSuccess)
            {
                ViewBag.Error = "Email veya password hatalı..!";
                return View(loginViewModel);
            }
            if (result.Value?.Token is null)
            {
                ViewBag.Info = "Kullanıcı Bulunamadı..!";
                return View(loginViewModel);
            }

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
            if(result.IsSuccess)
            {
                ViewBag.Info = "Mail Gönderildi...";
            }

            return View();
        }
        [Route("/renew-password/{verificationCode}")]
        [HttpGet]
        public IActionResult RenewPassword([FromRoute] string verificationCode)
        {
            if (!ModelState.IsValid || string.IsNullOrEmpty(verificationCode))
            {
                return RedirectToAction(nameof(ForgotPassword));
            }

            return View(new RenewPasswordViewModel
            {
                Email = string.Empty,
                Token = verificationCode,
                Password = string.Empty,
                ConfirmPassword = string.Empty,
            });
        }
        [Route("/renew-password/{verificationCode}")]
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
                PasswordHash = reNewPasswordViewModel.Password,
                PasswordRepeat = reNewPasswordViewModel.ConfirmPassword
            };

            var result = await service.RenewPasswordAsync(resetPasswordDTO);

            if(!result.IsSuccess)
            {
                ModelState.AddModelError(string.Empty, "Invalid code or email");
            }

            return RedirectToAction("Index","Home");
        }
        [HttpGet]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("auth-token");
            return RedirectToAction("Index","Home");
        }
        private async Task DoLoginAsync(UserEntity user)
        {
            if (user == null)
            {
                return;
            }

            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Name, user.UserName),
                new(ClaimTypes.Surname, user.UserSurName),
                new(ClaimTypes.Email, user.Email),
                new(ClaimTypes.Role, user.Role.Name),
                new("RoleId", user.RoleId.ToString()),
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddHours(1),
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authProperties);
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
