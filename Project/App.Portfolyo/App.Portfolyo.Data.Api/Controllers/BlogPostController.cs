using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolyoApp.Business.DTOs;
using PortfolyoApp.Data.Entities;
using PortfolyoApp.Data.Infrastructure;

namespace PortfolyoApp.Data.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostController(IDataRepository repo) : ControllerBase
    {
        [Route("list")]
        [HttpGet]
        public async Task<IActionResult> GetBlog()
        {
            var blogPosts = await repo.GetAll<BlogPostEntity>().ToListAsync();
            if (blogPosts is null)
            {
                return NotFound();
            }
            var dto = blogPosts.Select(u => new BlogPostDTO
            {
                Id = u.Id,
                Title = u.Title,
                Content = u.Content,
                ImageUrl = u.ImageUrl,
                CreatedAt = DateTime.UtcNow,
                PublishDate = DateTime.UtcNow,

            }).ToList();

            return Ok(dto);
        }
        [Route("add")]
        [HttpPost]
        public async Task<IActionResult> AddBlogPost(BlogPostDTO blogPostDTO)
        {
            var blogPost = new BlogPostEntity
            {
                Title = blogPostDTO.Title,
                Content = blogPostDTO.Content,
                ImageUrl = blogPostDTO.ImageUrl,
                PublishDate = DateTime.Now,
                CreatedAt = DateTime.Now
            };

            await repo.Add(blogPost);

            return Ok(blogPost);
        }
        [Route("detail/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetBlogPost(long id)
        {
            var post = await repo.GetById<BlogPostEntity>(id);
            if (post is null)
            {
                return NotFound();
            }
            var dto = new BlogPostDTO
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                ImageUrl = post.ImageUrl,
                CreatedAt = DateTime.Now,
                PublishDate = DateTime.Now
            };
            return Ok(dto);
        }
        [Route("edit/{id}")]
        [HttpPut]
        public async Task<IActionResult> EditBlogPost(BlogPostDTO blogPostDTO, long id)
        {
            var blogPost = await repo.GetById<BlogPostEntity>(id);
            if (blogPost is null)
            {
                return NotFound();
            }
            blogPost.Title = blogPostDTO.Title;
            blogPost.Content = blogPostDTO.Content;
            blogPost.ImageUrl = blogPostDTO.ImageUrl;

            await repo.Update(blogPost);

            return Ok(blogPost);
        }
        [Route("delete/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteBlogPost(long id)
        {
            var blogPost = await repo.GetById<BlogPostEntity>(id);
            if (blogPost is null)
            {
                return NotFound();
            }

            await repo.Delete(blogPost);

            return Ok();
        }
    }
}
