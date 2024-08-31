using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortfolyoApp.Business.DTOs;
using PortfolyoApp.Business.Services;
using PortfolyoApp.Business.Services.Abstract;
using PortfolyoApp.Mvc.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace PortfolyoApp.Mvc.Controllers
{
    [Authorize]
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
        //public async Task<IActionResult> AddComment(CommentViewModel commentViewModel, long id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(commentViewModel);
        //    }

        //    var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        //    var handler = new JwtSecurityTokenHandler();
        //    var jsonToken = handler.ReadToken(token);
        //    var tokenS = handler.ReadToken(token) as JwtSecurityToken;
        //    if (tokenS is null)
        //    {
        //        throw new InvalidOperationException("Token is null.");
        //    }
        //    var userId = tokenS.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value;
        //    var userName = tokenS.Claims.First(claim => claim.Type == ClaimTypes.Name).Value;
        //    var userSurname = tokenS.Claims.First(claim => claim.Type == ClaimTypes.Surname).Value;


        //    var userNames = $"{userName} {userSurname}";
        //    var blogId = await service.DetailAsyncBlog(id);
        //    var dto = new CommentDTO
        //    {
        //        BlogPostId = blogId.Id,
        //        UserId = Convert.ToInt64(userId),
        //        Content = commentViewModel.Content,
        //        UserName = userNames,
        //    };

        //    var result = await service.AddCommentAsync(dto, blogId.Id);

        //    if (result != null)
        //    {
        //        ViewBag.Success = "Yorum başarılı bir şekilde eklendi.";
        //    }
        //    else
        //    {
        //        ViewBag.Error = "Yorum eklenemedi.";
        //    }
        //    return View();
        //}
    }
}
