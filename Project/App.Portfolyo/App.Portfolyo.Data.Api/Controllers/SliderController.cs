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
    public class SliderController(IDataRepository repo) : ControllerBase
    {
        [Route("list")]
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var sliders = await repo.GetAll<SliderEntity>().ToListAsync();

            if (sliders is null)
            {
                return NotFound();
            }
            var dto = sliders.Select(u => new SlidersDTO
            {
                Id = u.Id,
                ImgUrl1 = u.ImgUrl1,
                ImgUrl2 = u.ImgUrl2,
            }).ToList();

            return Ok(sliders);
        }
        [Route("get/{id}")]
        [HttpGet]
        public async Task<IActionResult> Get(long id)
        {
            var slider = await repo.GetById<SliderEntity>(id);
            if (slider is null)
            {
                return NotFound();
            }
            var dto = new SlidersDTO
            {
                Id = slider.Id,
                ImgUrl1 = slider.ImgUrl1,
                ImgUrl2 = slider.ImgUrl2,
            };

            return Ok(dto);
        }
        [Route("edit/{id}")]
        [HttpPut]
        public async Task<IActionResult> Edit(long id, SlidersDTO sliderDTO)
        {
            var slider = await repo.GetById<SliderEntity>(id);
            if (slider is null)
            {
                return NotFound();
            }
            slider.ImgUrl1 = sliderDTO.ImgUrl1;
            slider.ImgUrl2 = sliderDTO.ImgUrl2;

            await repo.Update(slider);

            return Ok(slider);
        }
    }
}
