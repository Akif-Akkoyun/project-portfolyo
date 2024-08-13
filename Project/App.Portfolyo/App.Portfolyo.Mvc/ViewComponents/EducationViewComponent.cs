using Microsoft.AspNetCore.Mvc;
using PortfolyoApp.Business.Services;
using PortfolyoApp.Mvc.Models;

namespace PortfolyoApp.Mvc.ViewComponents
{
    public class EducationViewComponent(IUserService service) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var education = await service.ListAsyncEducation();

            if (education is null)
            {
                ViewBag.Message = "There is no data";
                return View(ViewBag.Message);
            }
            var result = education.Select(u => new EducationViewModel
            {
                Id = u.Id,
                Degree = u.Degree,
                School = u.School,
                StartDate = u.StartDate,
                EndDate = u.EndDate,
                Description = u.Description,
                CreatedAt = u.CreatedAt,
            }).ToList();

            return View(result);
        }
    }
}
