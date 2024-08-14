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
    public class ContactController(IDataRepository repo) : ControllerBase
    {
        [HttpGet("list")]
        public async Task<IActionResult> GetContact()
        {

           var contact = await repo.GetAll<ContactMessagesEntity>().ToListAsync();
            if (contact is null)
            {
                return NotFound();
            }
            var dto = contact.Select(u => new ContactDTO
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email,
                Message = u.Message,
                Subject = u.Subject,
                SentDate = u.SentDate,
                CreatedAt = DateTime.Now
            }).ToList();

            return Ok(dto);
        }
        [Route("add")]
        [HttpPost]
        public async Task<IActionResult> AddContact(ContactDTO contactDTO)
        {
            var contact = new ContactMessagesEntity
            {
                Name = contactDTO.Name,
                Email = contactDTO.Email,
                Message = contactDTO.Message,
                Subject = contactDTO.Subject,
                SentDate = contactDTO.SentDate
            };

            await repo.Add(contact);

            return Ok(contact);
        }
        [Route("delete/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteContact(int id)
        {
            var contact = await repo.GetById<ContactMessagesEntity>(id);
            if (contact is null)
            {
                return NotFound();
            }
            await repo.Delete(contact);
            return Ok();
        }
    }
}
