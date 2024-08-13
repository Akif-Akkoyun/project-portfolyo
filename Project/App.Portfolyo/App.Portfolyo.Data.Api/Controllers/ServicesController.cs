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
    public class ServicesController(IDataRepository repo) : ControllerBase
    {
        [Route("list")]
        [HttpGet]
        public async Task<IActionResult> ListService()
        {
            var serviceList = await repo.GetAll<ServiceEntity>().ToListAsync();

            if(serviceList is null)
            {
                return NotFound();
            }
            var dto = serviceList.Select(x => new ServiceDTO
            {
                Id = x.Id,
                Name = x.Name,
                CreatedAt = DateTime.Now,
            }).ToList();
            return Ok(dto);
        }
        [Route("add")]
        [HttpPost]
        public async Task<IActionResult> AddService(ServiceDTO serviceDTO)
        {
            var services = new ServiceEntity
            {
                Name = serviceDTO.Name,
                CreatedAt = DateTime.Now,
            };
            await repo.Add(services);
            return Ok(services);
        }
        [Route("edit/{id}")]
        [HttpPost]
        public async Task<IActionResult> EditService(long id,ServiceDTO serviceDTO)
        {
            var services = await repo.GetById<ServiceEntity>(id);

            if(services is null)
                 return NotFound();

            services.Name = serviceDTO.Name;

            await repo.Update(services);
            return Ok(services);
        }
        [Route("delete/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteService(long id)
        {
            var service = await repo.GetById<ServiceEntity>(id);

            if (service is null)
                return NotFound();
            await repo.Delete(service);
            return Ok();
        }
    }
}
