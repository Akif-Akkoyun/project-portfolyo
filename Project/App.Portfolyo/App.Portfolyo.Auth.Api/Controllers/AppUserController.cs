using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PortfolyoApp.Business.DTOs;
using PortfolyoApp.Data.Entities;
using PortfolyoApp.Data.Infrastructure;

namespace PortfolyoApp.Auth.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppUserController(IDataRepository repo) : ControllerBase
    {
        [Route("list")]
        [HttpGet]
        public IActionResult GetUser()
        {
            var users = repo.GetAll<UserEntity>().ToList();
            if (users is null)
            {
                return NotFound();
            }
            var dto = users.Select(u => new AppUserDTO
            {
                Id = u.Id,
                UserName = u.UserName,
                UserSurName = u.UserSurName,
                Email = u.Email,
                PasswordHash = u.PasswordHash,
                RoleId = u.RoleId,
            }).ToList();

            return Ok(dto);
        }
        [Route("get/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetUserById(long id)
        {
            var user = await repo.GetById<UserEntity>(id);
            if (user is null)
            {
                return NotFound();
            }

            var dto = new AppUserDTO
            {
                Id = user.Id,
                UserName = user.UserName,
                UserSurName = user.UserSurName,
                Email = user.Email,
                PasswordHash = user.PasswordHash,
                RoleId = user.RoleId,
            };

            return Ok(dto);
        }
        [Route("add")]
        [HttpPost]
        public async Task<IActionResult> AddUser(AppUserDTO userDTO)
        {
            var newUser = new UserEntity
            {
                UserName = userDTO.UserName,
                UserSurName = userDTO.UserSurName,
                Email = userDTO.Email,
                RoleId = userDTO.RoleId,
                PasswordHash = userDTO.PasswordHash
            };

            await repo.Add(newUser);

            return Ok(newUser);
        }
        [Route("edit/{id}")]
        [HttpPost]
        public async Task<IActionResult> EditUser(AppUserDTO userDTO, long id)
        {
            var user = await repo.GetById<UserEntity>(id);

            if (user is null)
            {
                return NotFound();
            }
            user.UserName = userDTO.UserName;
            user.UserSurName = userDTO.UserSurName;
            user.Email = userDTO.Email;
            user.RoleId = userDTO.RoleId;
            user.PasswordHash = userDTO.PasswordHash;

            await repo.Update(user);

            return Ok(user);
        }
        [Route("delete/{id}")]
        [HttpPost]
        public async Task<IActionResult> DeleteUser(long id)
        {
            var user = await repo.GetById<UserEntity>(id);
            if (user is null)
            {
                return NotFound();
            }

            await repo.Delete(user);

            return Ok(user);
        }
    }
}
