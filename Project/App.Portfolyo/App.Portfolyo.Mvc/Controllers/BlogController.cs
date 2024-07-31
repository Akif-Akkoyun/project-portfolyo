using Microsoft.AspNetCore.Mvc;
using PortfolyoApp.Business.Services;
using PortfolyoApp.Mvc.Models;

namespace PortfolyoApp.Mvc.Controllers
{
    public class BlogController(UserService userService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var users = await userService.GetUser();

            var viewModel = users.Select(u => new UserViewModel
            {
                Id = u.Id,
                Name = u.Name,
                Surname = u.Surname,
                Email = u.Email,
                Password = u.Password
            }).ToList();

            return View(viewModel);
        }
    }
}
