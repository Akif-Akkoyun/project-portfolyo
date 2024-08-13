using Microsoft.AspNetCore.Mvc;
using PortfolyoApp.Business.Services;
using PortfolyoApp.Mvc.Models;
using ServiceStack;

namespace PortfolyoApp.Mvc.ViewComponents
{
    public class AboutMeViewComponent(IUserService service) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var listAbout = await service.GetList();

            if (listAbout is null)
            {
                ViewBag.Message = "There is no data";
                return View(ViewBag.Message);
            }
            var result = listAbout.Select(u => new AboutMeViewModel
            {
                Id = u.Id,
                ImageUrl1 = u.ImageUrl1,
                Introduction = u.Introduction,
                Name = u.Name,
                PhoneNumber = u.PhoneNumber,
                Email = u.Email,
                Address = u.Address,
                DateOfbirth = u.DateOfbirth,
                CreatedAt = u.CreatedAt,
                ZipCode = u.ZipCode
            }).ToList();
            return View(result);
        }
    }
}
