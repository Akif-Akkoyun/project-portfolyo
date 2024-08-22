using Microsoft.AspNetCore.Mvc;
using PortfolyoApp.Business.DTOs;
using PortfolyoApp.Business.Services;
using PortfolyoApp.Mvc.Models;

namespace PortfolyoApp.Mvc.Controllers
{
    public class BlogPostController(IUserService service) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> DetailBlog([FromRoute] long id)
        {
            if (id <= 0) // Simple validation for the id
            {
                return BadRequest("Invalid blog post ID.");
            }
            var result = await service.DetailAsyncBlog(id);

            if (result is null)
            {
                return NotFound();
            }

            var model = new BlogPostViewModel
            {
                Id = result.Id,
                Title = result.Title,
                Content = result.Content,
                ImageUrl = result.ImageUrl,
                CreatedAt = result.CreatedAt
            };

            return View(model);
        }
    }
}
