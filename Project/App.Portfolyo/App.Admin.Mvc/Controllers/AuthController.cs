using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortfolyoApp.Admin.Mvc.Models;
using PortfolyoApp.Business.DTOs.Auth;
using System.Security.Claims;
using PortfolyoApp.Data.Entities;
using System.IdentityModel.Tokens.Jwt;
using PortfolyoApp.Business.Services.Abstract;
using ServiceStack.Script;


namespace PortfolyoApp.Admin.Mvc.Controllers
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

            Response.Cookies.Append("auth-token", result.Value.Token);

            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("auth-token");
            return RedirectToAction("Login");
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
    }
}
