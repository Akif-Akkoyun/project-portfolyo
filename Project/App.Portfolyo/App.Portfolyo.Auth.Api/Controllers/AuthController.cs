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
        public async Task<IActionResult> LoginAsync(LoginDTO loginDTO)
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

            var tokenDto = new TokenDTO
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
    }
}
