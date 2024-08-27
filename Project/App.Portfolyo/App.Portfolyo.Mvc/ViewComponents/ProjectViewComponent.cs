using Microsoft.AspNetCore.Mvc;
using PortfolyoApp.Business.Services;
using PortfolyoApp.Mvc.Models;

namespace PortfolyoApp.Mvc.ViewComponents
{
    public class ProjectViewComponent(IUserService service) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var projects = await service.ListAsyncProject();

            if (projects is null)
            {
                ViewBag.Message = "There is no data";
                return View(ViewBag.Message);
            }
            var result = projects
                .OrderByDescending(u => u.Id)
                .Take(6)
                .Select(u => new ProjectViewModel
            {
                Id = u.Id,
                Title = u.Title,
                Description = u.Description,
                ImageUrl = u.ImageUrl,
                Url = u.Url,
                GithubUrl = u.GithubUrl,
                Tags = u.Tags,
                CreatedAt = u.CreatedAt
            }).ToList();

            return View(result);
        }
    }
}
