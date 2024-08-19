using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolyoApp.Business.DTOs;
using System.Security.Claims;
using PortfolyoApp.Business.DTOs.Auth;
using System.Data;
using Ardalis.Result;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using IdentityModel;
using Hasher = BCrypt.Net.BCrypt;
using FluentValidation;
using PortfolyoApp.Business.DTOs.Mail;
using PortfolyoApp.Business.Services.Abstract;
using PortfolyoApp.Data.Entities;
using ServiceStack.Auth;
using PortfolyoApp.Data.Infrastructure;

namespace PortfolyoApp.Auth.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IDataRepository _repo;
        private readonly IConfiguration _config;
        private readonly IServiceProvider _serviceProvider;

        public AuthController(IDataRepository repo, IConfiguration config, IServiceProvider serviceProvider)
        {
            _repo = repo;
            _config = config;
            _serviceProvider = serviceProvider;
        }
        //Admin Login
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            var user = await _repo.GetAll<UserEntity>()
                    .Include(u => u.Role)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(u => u.Email == loginDTO.Email && u.PasswordHash == loginDTO.PasswordHash);

            if (user is null)
            {
                ModelState.AddModelError("Email", "Email or password is incorrect");
                return BadRequest(ModelState);
            }

            if (user.RoleId != 1) // RoleId 1 = "Admin"
            {
                ModelState.AddModelError("Role", "Unauthorized Access !!");
                return Unauthorized(ModelState);
            }

            var tokenDto = new AuhtTokenDTO
            {
                Token = GenerateToken(user)
            };

            return Ok(tokenDto);
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            if (registerDTO is null)
            {
                throw new ArgumentNullException(nameof(registerDTO));
            }
            var user = new UserEntity
            {
                UserName = registerDTO.UserName,
                UserSurName = registerDTO.UserSurName,
                Email = registerDTO.Email,
                PasswordHash = Hasher.HashPassword(registerDTO.PasswordHash),
                CreatedAt = registerDTO.CreatedAt,
                RoleId = 2
            };

            await _repo.Add(user);

            return Ok(user);
        }
        [HttpPost("forgot-password")]
        public async Task<Result> ForgotPassword(ForgotPasswordDTO forgotPasswordDTO)
        {
            var user = await _repo.GetAll<UserEntity>()
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Email == forgotPasswordDTO.Email);

            if (user is null)
            {
                return Result.NotFound();
            }

            user.RefreshPasswordToken = Guid.NewGuid().ToString("n");

            var mailSend = new MailSendDTO
            {
                To = [user.Email],
                Subject = "Şifre Sıfırlama",
                Body = $"Merhaba {user.UserName.ToUpper()},<br>Your reset password code: <strong>{user.RefreshPasswordToken}</strong>.</br>",
                IsHtml = true
            };

            var mailService = HttpContext.RequestServices.GetRequiredService<IMailService>();

            var mailSender = await mailService.SendMailAsync(mailSend);

            if (!mailSender.IsSuccess)
            {
                return Result.Invalid(new ValidationError("Mail could not be sent"));
            }
            await _repo.Update(user);

            return Result.Success();
        }
        [HttpPost("renew-password")]
        public async Task<Result> RenewPassword(ResetPasswordDTO resetPasswordDto)
        {
            var validationResult = await ValidateModelAsync(resetPasswordDto);
            if (!validationResult.IsSuccess)
            {
                return validationResult;
            }

            var user = await _repo.GetAll<UserEntity>()
                .SingleOrDefaultAsync(x =>
                    x.RefreshPasswordToken == resetPasswordDto.Token
                    && x.Email == resetPasswordDto.Email);

            if (user is null)
            {
                return Result.NotFound();
            }

            user.PasswordHash = Hasher.HashPassword(resetPasswordDto.PasswordHash);
            user.RefreshPasswordToken = null;

            await _repo.Update(user);

            return Result.Success();
        }
        private string GenerateToken(UserEntity user)
        {
            var claims = new List<Claim>
            {
                new(JwtClaimTypes.Subject, user.Id.ToString()),
                new(JwtClaimTypes.Name, user.UserSurName),
                new(JwtClaimTypes.FamilyName, user.UserSurName),
                new(JwtClaimTypes.Email, user.Email),
                new(JwtClaimTypes.Role, user.Role.Name),
            };

            string secret = _config.GetRequiredSection("JWT:Secret").Get<string>()!;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            string issuer = _config.GetRequiredSection("Jwt:Issuer").Get<string>()!;

            var token = new JwtSecurityToken(
                issuer,
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials
            );
            Response.Cookies.Append("access_token", new JwtSecurityTokenHandler().WriteToken(token), new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.Now.AddMinutes(30)
            });

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        protected T? GetService<T>() => _serviceProvider.GetService<T>();
        protected virtual async Task<Result> ValidateModelAsync<T>(T model)
        {
            var validator = GetService<IValidator<T>>();
            if (validator is not null)
            {
                var validationResult = await validator.ValidateAsync(model);
                if (!validationResult.IsValid)
                {
                    return Result.Invalid(validationResult.Errors.Select(x => new ValidationError(x.ErrorMessage)));
                }
            }
            return Result.Success();
        }
    }
}