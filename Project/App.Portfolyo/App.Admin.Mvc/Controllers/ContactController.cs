using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PortfolyoApp.Admin.Mvc.Models;
using PortfolyoApp.Business.DTOs;
using PortfolyoApp.Business.Services;
using ServiceStack;

namespace PortfolyoApp.Admin.Mvc.Controllers
{
    public class ContactController(IUserService service,IMapper mapper) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> ListContact()
        {
            var contactList = await service.ListAsyncContact();

            var model = contactList.Select(u => new ContactViewModel
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email,
                Message = u.Message,
                Subject = u.Subject,
                SentDate = u.SentDate,
                CreatedAt = DateTime.Now,
            }).ToList();

            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> DeleteContact([FromRoute] long id)
        {
            var result = await service.DeleteContact(id);

            return RedirectToAction("ListContact");
        }
        [HttpGet]
        public IActionResult DetailContact()
        {
            return View();
        }
    }
}
