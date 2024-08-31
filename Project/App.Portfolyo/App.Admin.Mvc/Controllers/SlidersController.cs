using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PortfolyoApp.Admin.Mvc.Models;
using PortfolyoApp.Business.DTOs;
using PortfolyoApp.Business.Services;

namespace PortfolyoApp.Admin.Mvc.Controllers
{
    public class SlidersController(IUserService service, IFileService fileService, IMapper mapper) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> ListSlider()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var sliderList = await service.GetSliderListAsync();
            var model = sliderList.Select(u => new SliderViewModel
            {
                Id = u.Id,
                ImgUrl1 = u.ImgUrl1,
                ImgUrl2 = u.ImgUrl2,
            }).ToList();

            return View(model);
        }
        [HttpGet]
        public IActionResult EditSlider()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> EditSlider([FromForm] SliderViewModel sliderViewModel)
        {
            if (!ModelState.IsValid)
            {
                try
                {
                    long id = 1;
                    var existingProject = await service.SliderGetIdAsync(id);
                    if (existingProject == null)
                    {
                        ViewBag.Error = "Proje bulunamadı";
                        return View(sliderViewModel);
                    }

                    if (sliderViewModel.ImgFile1 != null && sliderViewModel.ImgFile1.Length > 0 && sliderViewModel.ImgFile2 != null && sliderViewModel.ImgFile2.Length > 0)
                    {
                        if (!string.IsNullOrEmpty(existingProject.ImgUrl1) && !string.IsNullOrEmpty(existingProject.ImgUrl1))
                        {
                            await fileService.DeleteFileAsync(existingProject.ImgUrl1);
                            await fileService.DeleteFileAsync(existingProject.ImgUrl2);
                        }

                        var uploadResult1 = await fileService.UploadFileAsync(sliderViewModel.ImgFile1);
                        var uploadResult2 = await fileService.UploadFileAsync(sliderViewModel.ImgFile2);
                        if (uploadResult1.IsSuccess && uploadResult2.IsSuccess)
                        {
                            sliderViewModel.ImgUrl1 = uploadResult1.Value;
                            sliderViewModel.ImgUrl2 = uploadResult2.Value;
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Failed to upload file");
                            return View(sliderViewModel);
                        }
                    }

                    var dto = mapper.Map<SlidersDTO>(sliderViewModel);

                    var updateResult = await service.EditSliderAsync(dto, id);

                    if (updateResult != null)
                    {
                        ViewBag.Success = "Başarı ile güncellendi";
                    }
                    else
                    {
                        ViewBag.Error = "Güncelleme başarısız oldu";
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Failed to update project: " + ex.Message);
                }
            }

            return View(sliderViewModel);
        }
    }        
}
