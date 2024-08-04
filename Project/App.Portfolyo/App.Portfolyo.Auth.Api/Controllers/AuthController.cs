using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolyoApp.Auth.Api.Data;
using PortfolyoApp.Auth.Api.Data.Entites;
using PortfolyoApp.Business.DTOs;
using System.Security.Claims;
using PortfolyoApp.Business.DTOs.Auth;
using System.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity.Data;
using Ardalis.Result;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using IdentityModel;
using ImTools;
using Hasher = BCrypt.Net.BCrypt;
using FluentValidation;
using PortfolyoApp.Business.DTOs.Mail;
using Microsoft.Extensions.DependencyInjection;
using System;
using PortfolyoApp.Business.Services;
using Microsoft.VisualBasic;

namespace PortfolyoApp.Auth.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;
        private readonly IConfiguration _config;
        private readonly IServiceProvider _serviceProvider;

        public AuthController(IAuthRepository auhtRepository,IConfiguration config,IServiceProvider serviceProvider)
        {
            _authRepository = auhtRepository;
            _config = config;
            _serviceProvider = serviceProvider;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            var user = await _authRepository.GetAll<UserEntity>()
                .Include(u => u.Role)
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == loginDTO.Email);
            if (user is null)
            {
                return NotFound("Kullanıcı Bulunamadı !!");
            }
            var token = GenerateToken(user);

            var tokenDto = new AuhtTokenDTO
            {
                Token = token
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

            await _authRepository.Add(user);

            return Ok(user);
        }
        [HttpPost("forgot-password")]
        public async Task<Result> ForgotPassword(ForgotPasswordDTO forgotPasswordDTO)
        {
            var user = await _authRepository.GetAll<UserEntity>()
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
                Body = $"Merhaba {user.UserName.ToUpper()}, <br> Şifrenizi sıfırlamak için <a href='https://localhost:5001/reset-password/{user.RefreshPasswordToken}'>tıklayınız</a>",
                IsHtml = true
            };

            var mailService = HttpContext.RequestServices.GetRequiredService<IMailService>();

            var mailSender = await mailService.SendMailAsync(mailSend);

            if (!mailSender.IsSuccess)
            {
                return Result.Invalid(new ValidationError("Mail could not be sent"));
            }
            await _authRepository.Update(user);

            return Result.Success();
        }
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDTO resetPasswordDTO)
        {
            var user = await _authRepository.GetAll<UserEntity>()
                .Include(u => u.Role)
                .AsNoTracking()
                .SingleOrDefaultAsync(u =>
                u.RefreshPasswordToken == resetPasswordDTO.Token
                && u.Email == resetPasswordDTO.Email);

            if (user is null)
            {
                return NotFound("Kullanıcı Bulunamadı !!");
            }

            user.PasswordHash = Hasher.HashPassword(resetPasswordDTO.PasswordHash);
            user.RefreshPasswordToken = null;

            await _authRepository.Update(user);

            return Ok(user);
        }
        public async Task<Result<RefreshTokenResulttDTO>> RefreshToken(RefreshTokenRequestDTO refreshTokenRequestDto)
        {
            var validate = await ValidateModelAsync(refreshTokenRequestDto);
            if (!validate.IsSuccess)
            {
                return validate;
            }
            JwtSecurityToken jwt;

            try
            {
                jwt = new JwtSecurityTokenHandler().ReadJwtToken(refreshTokenRequestDto.Token);
            }
            catch (Exception)
            {
                return Result.Invalid(new ValidationError("Token is not valid"));
            }

            var userId = jwt.Claims.FirstOrDefault(x => x.Type == JwtClaimTypes.Subject)?.Value;

            if (!int.TryParse(userId ?? string.Empty, out var id))
            {
                return Result.Invalid(new ValidationError("Token is not valid"));
            }

            var user = await _authRepository.GetAll<UserEntity>()
                .Include(u => u.Role)
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user is null)
            {
                return Result.Invalid(new ValidationError("User not found"));
            }

            var refreshToken = new RefreshTokenResulttDTO
            {
                Token = GenerateToken(user)
            };

            return Result.Success(refreshToken);

        }
        [HttpPost("logout")]
        public Task<IActionResult> Logout()
        {
            Response.Cookies.Delete("access_token");
            return Task.FromResult<IActionResult>(Ok());
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
