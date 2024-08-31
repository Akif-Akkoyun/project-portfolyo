using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PortfolyoApp.Admin.Mvc.Models;
using PortfolyoApp.Business.DTOs;
using PortfolyoApp.Business.Services;

namespace PortfolyoApp.Admin.Mvc.Controllers
{
    public class BlogPostController(IUserService service,IFileService fileService,IMapper mapper) : Controller
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
                CreatedAt = DateTime.Now
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

                if (blogPostViewModel.ImageFile != null && blogPostViewModel.ImageFile.Length > 0)
                {
                    var uploadResult = await fileService.UploadFileAsync(blogPostViewModel.ImageFile);

                    if (uploadResult.IsSuccess)
                    {
                        var filePath = uploadResult.Value;

                        var blogDto = mapper.Map<BlogPostDTO>(blogPostViewModel);

                        try
                        {
                            blogDto.ImageUrl = filePath;

                            var result = await service.AddAsyncBlog(blogDto);

                            if (result != null)
                            {
                                ViewBag.Success = "Başarı ile eklendi";
                            }
                            else
                            {
                                ViewBag.Error = "Ekleme başarısız oldu";
                            }

                            return View();
                        }
                        catch (Exception ex)
                        {
                            ModelState.AddModelError(string.Empty, "Failed to add project: " + ex.Message);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Failed to upload file");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Please select a file to upload.");
                }
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
                try
                {
                    var existingProject = await service.DetailAsyncBlog(id);
                    if (existingProject == null)
                    {
                        ViewBag.Error = "Proje bulunamadı";
                        return View(blogPostViewModel);
                    }

                    if (blogPostViewModel.ImageFile != null && blogPostViewModel.ImageFile.Length > 0)
                    {
                        if (!string.IsNullOrEmpty(existingProject.ImageUrl))
                        {
                            await fileService.DeleteFileAsync(existingProject.ImageUrl);
                        }

                        var uploadResult = await fileService.UploadFileAsync(blogPostViewModel.ImageFile);
                        if (uploadResult.IsSuccess)
                        {
                            blogPostViewModel.ImageUrl = uploadResult.Value;
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Failed to upload file");
                            return View(blogPostViewModel);
                        }
                    }

                    var dto = mapper.Map<BlogPostDTO>(blogPostViewModel);

                    var updateResult = await service.EditAsyncBlog(dto, id);

                    if (updateResult != null)
                    {
                        ViewBag.Success = "Başarı ile güncellendi";
                    }
                    else
                    {
                        ViewBag.Error = "Güncelleme başarısız oldu";
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Failed to update project: " + ex.Message);
                }
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
