using Microsoft.AspNetCore.Mvc;
using PortfolyoApp.Business.Services;
using PortfolyoApp.Mvc.Models;

namespace PortfolyoApp.Mvc.ViewComponents
{
    public class ServiceViewComponent(IUserService service) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var listService = await service.ListAsyncService();

            if (listService is null)
            {
                ViewBag.Message = "There is no data";
                return View(ViewBag.Message);
            }
            var result = listService.Select(u => new ServiceViewModel
            {
                Id = u.Id,
                Name = u.Name,                
                CreatedAt = u.CreatedAt
            }).ToList();
            return View(result);
        }
    }
}
