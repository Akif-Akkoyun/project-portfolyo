using Microsoft.AspNetCore.Mvc;
using PortfolyoApp.Business.DTOs;
using PortfolyoApp.Business.Services;
using PortfolyoApp.Mvc.Models;

namespace PortfolyoApp.Mvc.Controllers
{
    public class ContactMeController(IUserService service) : Controller
    {
        [HttpGet]
        public IActionResult AddMessage()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddMessage(ContactViewModel contactViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(contactViewModel);
            }

            var dto = new ContactDTO
            {
                Name = contactViewModel.Name,
                Email = contactViewModel.Email,
                Subject = contactViewModel.Subject,
                Message = contactViewModel.Message,
                CreatedAt = DateTime.Now
            };

            var result = await service.AddAsyncContact(dto);

            if (result != null)
            {
                ViewBag.Success = "Mesaj başarılı bir şekilde gönderildi, en kısa zamanda geri dönüş yapılacaktır.";
            }
            else
            {
                ViewBag.Error = "Mesaj gönderilemedi.";
            }
            return View();
        }
    }
}
