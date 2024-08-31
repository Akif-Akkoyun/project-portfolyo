using Microsoft.AspNetCore.Mvc;
using PortfolyoApp.Business.Services;
using PortfolyoApp.Mvc.Models;
using ServiceStack;

namespace PortfolyoApp.Mvc.ViewComponents
{
    public class SliderViewComponent(IUserService service) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var listAbout = await service.GetSliderListAsync();

            if (listAbout is null)
            {
                ViewBag.Message = "There is no data";
                return View(ViewBag.Message);
            }
            var result = listAbout.Select(u => new SliderViewModel
            {
                Id = u.Id,
                ImgUrl1 = u.ImgUrl1,
                ImgUrl2 = u.ImgUrl2,
            }).ToList();

            return View(result);
        }

    }
}
