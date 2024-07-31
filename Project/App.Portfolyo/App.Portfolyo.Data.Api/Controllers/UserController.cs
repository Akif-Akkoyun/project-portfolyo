using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PortfolyoApp.Data.Entities;
using PortfolyoApp.Data.Infrastructure;
using PortfolyoApp.Data;
using PortfolyoApp.Business.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using PortfolyoApp.Business;

namespace PortfolyoApp.Data.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IDataRepository _dataRepository;

        public UserController(IDataRepository dataRepository)
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
                Name = u.Name,
                Surname = u.Surname,
                Email = u.Email,
                Password = u.Password
            }).ToList();

            return Ok(userDtos);
        }
    }
}
