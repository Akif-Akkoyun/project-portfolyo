using Microsoft.AspNetCore.Mvc;
using PortfolyoApp.Admin.Mvc.Models;
using PortfolyoApp.Business.DTOs;
using PortfolyoApp.Business.Services;

namespace PortfolyoApp.Admin.Mvc.Controllers
{
    public class BlogPostController(IUserService service) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> ListBlog()
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }
            var blogList = await service.ListAsyncBlog();
            var model = blogList.Select(u => new BlogPostViewModel
            {
                Id = u.Id,
                Title = u.Title,
                Content = u.Content,
                ImageUrl = u.ImageUrl,
                PublishDate = DateTime.Now,
                CreatedAt = DateTime.Now,
            }).ToList();

            return View(model);
        }
        [HttpGet]
        public IActionResult AddBlogPost()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddBlogPost([FromForm] BlogPostViewModel blogPostViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(blogPostViewModel);
            }
            var dto = new BlogPostDTO
            {
                Title = blogPostViewModel.Title,
                Content = blogPostViewModel.Content,
                ImageUrl = blogPostViewModel.ImageUrl,
                CreatedAt = DateTime.UtcNow,
            };
            var result = await service.AddAsyncBlog(dto);
            if (result != null)
            {
                return RedirectToAction("ListBlog");
            }
            return View(blogPostViewModel);
        }
        [HttpGet]
        public IActionResult EditBlogPost()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> EditBlogPost([FromForm] BlogPostViewModel blogPostViewModel,long id)
        {
            if (!ModelState.IsValid)
            {
                return View(blogPostViewModel);
            }
            var dto = new BlogPostDTO
            {
                Title = blogPostViewModel.Title,
                Content = blogPostViewModel.Content,
                ImageUrl = blogPostViewModel.ImageUrl,
                CreatedAt = DateTime.UtcNow,
            };
            var result = await service.EditAsyncBlog(dto, id);
            if (result != null)
            {
                ViewBag.Success = "Başarı ile güncellendi";
            }
            else
            {
                ViewBag.Error = "Güncelleme başarısız oldu";
            }
            return View(blogPostViewModel);
        }
        [HttpGet]
        public async Task<IActionResult> DeleteBlogPost(long id)
        {
            var result = await service.DeleteAsyncBlog(id);

            if (result != null)
            {
                ViewBag.Success = "Başarı ile silindi";
            }
            else
            {
                ViewBag.Error = "Silme başarısız oldu";
            }
            
            return RedirectToAction("ListBlog");
        }
    }
}
