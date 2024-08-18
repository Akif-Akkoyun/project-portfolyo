using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortfolyoApp.Admin.Mvc.Models;
using PortfolyoApp.Business.DTOs.Auth;
using System.Security.Claims;
using PortfolyoApp.Data.Entities;
using System.IdentityModel.Tokens.Jwt;
using PortfolyoApp.Admin.Mvc.Controllers;
using PortfolyoApp.Business.Services.Abstract;


namespace PortfolyoApp.Mvc.Controllers
{
    [AllowAnonymous]
    public class AuthController(IAuthService service) : Controller
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
            var result = await service.LoginAsync(dto);
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

            LogInAsync(result.Value.Token);

            return RedirectToAction("Index", "Home");


        }
        //[HttpGet]
        //public IActionResult Register()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public async Task<IActionResult> Register([FromForm] RegisterViewModel registerViewModel)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(registerViewModel);
        //    }

        //    var source = mapper.Map<RegisterViewModel, RegisterDTO>(registerViewModel);

        //    var registerDTO = new RegisterDTO
        //    {
        //        UserName = registerViewModel.Name,
        //        UserSurName = registerViewModel.SurName,
        //        Email = registerViewModel.Email,
        //        PasswordHash = registerViewModel.Password
        //    };
        //    var result = await service.RegistersAsync(registerDTO);

        //    return RedirectToAction("Login", "Auth");
        //}
        //[HttpGet]
        //public IActionResult ForgotPassword()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public async Task<IActionResult> ForgotPassword([FromForm] ForgotPasswordViewModel forgotPasswordViewModel)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(forgotPasswordViewModel);
        //    }

        //    var source = mapper.Map<ForgotPasswordViewModel, ForgotPasswordDTO>(forgotPasswordViewModel);

        //    var forgotPasswordDTO = new ForgotPasswordDTO
        //    {
        //        Email = forgotPasswordViewModel.Email
        //    };

        //    var result = await service.ForgotPasswordAsync(forgotPasswordDTO);

        //    return RedirectToAction(nameof(RenewPassword));
        //}
        //[HttpGet]
        //public IActionResult RenewPassword()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public async Task<IActionResult> RenewPassword([FromForm] RenewPasswordViewModel reNewPasswordViewModel)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(reNewPasswordViewModel);
        //    }

        //    var source = mapper.Map<RenewPasswordViewModel, ResetPasswordDTO>(reNewPasswordViewModel);

        //    var resetPasswordDTO = new ResetPasswordDTO
        //    {
        //        Token = reNewPasswordViewModel.Token,
        //        Email = reNewPasswordViewModel.Email,
        //        PasswordHash = reNewPasswordViewModel.Password

        //    };

        //    var result = await service.RenewPasswordAsync(resetPasswordDTO);

        //    if (!result.IsSuccess)
        //    {
        //        ModelState.AddModelError(string.Empty, "Invalid code or email");
        //    }

        //    return RedirectToAction("Index", "Home");
        //}
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
        private void LogInAsync(string token)
        {
            Response.Cookies.Append("auth-token", token);
        }

    }
}
