using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolyoApp.Auth.Api.Data;
using PortfolyoApp.Auth.Api.Data.Entites;
using PortfolyoApp.Business.DTOs;
using System.Security.Claims;

namespace PortfolyoApp.Auth.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _dataRepository;

        public AuthController(IAuthRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetUserList()
        {
            var users = await _dataRepository.GetAll<UserEntity>().ToListAsync();

            var userDtos = users.Select(u => new UserDTO
            {
                Id = u.Id,
                UserName = u.UserName,
                UserSurName = u.UserSurName,
                Email = u.Email,
                RoleId = u.RoleId,
                CreatedAt = u.CreatedAt,
                PasswordHash = u.PasswordHash
            }).ToList();

            return Ok(userDtos);
        }
        
    }
}
