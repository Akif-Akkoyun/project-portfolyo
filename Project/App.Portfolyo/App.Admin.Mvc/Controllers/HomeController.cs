using PortfolyoApp.Admin.Mvc;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using PortfolyoApp.Business.Services.Abstract;
using PortfolyoApp.Admin.Mvc.Models;
using PortfolyoApp.Business.Services;
using AutoMapper;
using PortfolyoApp.Business.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace PortfolyoApp.Admin.Mvc.Controllers
{
    public class HomeController(AuthService service) : Controller
    {

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userList = await service.UserListAsync();

            if (userList is null)
            {
                return NotFound();
            }

            var model = userList.Select(u => new UserViewModel
            {
                Id = u.Id,
                UserName = u.UserName,
                UserSurName = u.UserSurName,
                Email = u.Email,
                Role = u.Role,
                CreatedAt = u.CreatedAt
            }).ToList();


            return View(model);
        }
    }
}
