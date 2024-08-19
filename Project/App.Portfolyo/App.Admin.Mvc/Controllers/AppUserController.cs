using Microsoft.AspNetCore.Mvc;
using PortfolyoApp.Admin.Mvc.Models;
using PortfolyoApp.Business.DTOs;
using PortfolyoApp.Business.Services.Abstract;
using ServiceStack;

namespace PortfolyoApp.Admin.Mvc.Controllers
{
    public class AppUserController(IAuthService service) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> ListUser()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var usersList = await service.UserListAsync();

            if (usersList is null)
                return NotFound();
            
            var model = usersList.Select(u => new AppUserViewModel
            {
                Id = u.Id,
                UserName = u.UserName,
                UserSurName = u.UserSurName,
                Email = u.Email,
                PasswordHash = u.PasswordHash,
                RoleId = u.RoleId,
                CreatedAt= DateTime.Now,
            }).ToList();

            return View(model);
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromForm] AppUserViewModel userViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(userViewModel);
            }

            var dto = new AppUserDTO
            {
                UserName = userViewModel.UserName,
                UserSurName = userViewModel.UserSurName,
                Email = userViewModel.Email,
                PasswordHash = userViewModel.PasswordHash,
                RoleId = userViewModel.RoleId,
            };

            if(dto is null)
            {
                throw new InvalidOperationException("DTO is null");
            }
                
            var result = await service.AddUserAsync(dto);

            if (result != null)
            {
                ViewBag.Success = "Başarı ile eklendi";
            }
            else
            {
                ViewBag.Error = "Ekleme başarısız oldu";
            }
            return View(userViewModel);
        }
        [HttpGet]
        public IActionResult EditUser()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> EditUser([FromForm] AppUserViewModel userViewModel,[FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return View(userViewModel);
            }

            var dto = new AppUserDTO
            {
                UserName = userViewModel.UserName,
                UserSurName = userViewModel.UserSurName,
                Email = userViewModel.Email,
                RoleId = userViewModel.RoleId,
                PasswordHash = userViewModel.PasswordHash,
                CreatedAt = DateTime.UtcNow
            };

            if(dto is null)
            {
                throw new InvalidOperationException("DTO is null");
            }
                
            var result = await service.EditUserAsync(dto, id);

            if (result != null)
            {
                ViewBag.Success = "Başarı ile güncellendi";
            }
            else
            {
                ViewBag.Error = "Güncelleme başarısız oldu";
            }
            return View(userViewModel);
        }
        [HttpGet]
        public async Task<IActionResult> DeleteUser(long id)
        {
            var result = await service.UserDeleteAsync(id);

            return RedirectToAction("ListUser");
        }
    }
}
