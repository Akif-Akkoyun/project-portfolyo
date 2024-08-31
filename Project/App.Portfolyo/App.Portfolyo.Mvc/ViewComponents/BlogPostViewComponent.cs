using Microsoft.AspNetCore.Mvc;
using PortfolyoApp.Business.Services;
using PortfolyoApp.Mvc.Models;
using ServiceStack;

namespace PortfolyoApp.Mvc.ViewComponents
{
    public class BlogPostViewComponent(IUserService service) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var listAbout = await service.ListAsyncBlog();

            if (listAbout is null)
            {
                ViewBag.Message = "There is no data";
                return View(ViewBag.Message);
            }
            var result = listAbout
                .Take(3)
                .OrderByDescending(u => u.Id)
                .Select(u => new BlogPostViewModel
                {
                    Id = u.Id,
                    ImageUrl = u.ImageUrl,
                    Title = u.Title,
                    Content = u.Content,
                    CreatedAt = u.CreatedAt,
                }).ToList();

            return View(result);
        }
    }
}
