using Microsoft.AspNetCore.Mvc;
using PortfolyoApp.Business.Services;
using PortfolyoApp.Mvc.Models;

namespace PortfolyoApp.Mvc.ViewComponents
{
    public class ExperianceViewComponent(IUserService service) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var listExp = await service.ListAsyncExp();

            if (listExp is null)
            {
                ViewBag.Message = "There is no data";
                return View(ViewBag.Message);
            }
            var result = listExp.Select(u => new ExperienceViewModel
            {
                Id = u.Id,
                Title = u.Title,
                Company = u.Company,
                StartDate = u.StartDate,
                EndtDate = u.EndtDate,
                Description = u.Description,
                CreatedAt = u.CreatedAt
            }).ToList();
            return View(result);
        }
    }
}
