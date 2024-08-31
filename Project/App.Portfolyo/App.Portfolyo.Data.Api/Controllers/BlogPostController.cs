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
<<<<<<< Updated upstream
                CreatedAt = DateTime.UtcNow,
=======
                CreatedAt = DateTime.Now,
                PublishDate = DateTime.Now
>>>>>>> Stashed changes

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
                PublishDate = DateTime.UtcNow
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
                CreatedAt = DateTime.Now
            };
            return Ok(dto);
        }
        [Route("edit/{id}")]
        [HttpPost]
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
        [Route("add-comment/{id}")]
        [HttpPost]
        public async Task<IActionResult> AddComment(CommentDTO commentDTO,long id)
        {
            var comment = new CommentsEntity
            {
                BlogPostId = id,
                UserId = commentDTO.UserId,
                Content = commentDTO.Content,
                UserName = commentDTO.UserName,
                CreatedAt = DateTime.Now
            };

            await repo.Add(comment);

            return Ok(comment);
        }
    }
}
