using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PortfolyoApp.Business.Services;
using ServiceStack;
using System.Text.Encodings.Web;

namespace PortfolyoApp.File.Api.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        //private readonly string _filePath = null!;

        public FileController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("Dosya yüklenmemiş");
            }

            var rootPath = _webHostEnvironment.WebRootPath;
<<<<<<< Updated upstream
            
=======

>>>>>>> Stashed changes
            var fileName = file.FileName.Replace(" ", "-").Replace(")", "").Replace("(", "").ToLower();
            var filePath = Path.Combine(rootPath, "img", fileName);
            if (System.IO.File.Exists(filePath))
            {
<<<<<<< Updated upstream
               return BadRequest("Dosya zaten var");
=======
                return BadRequest("Dosya zaten var");
>>>>>>> Stashed changes
            }
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return Ok(new { filePath = $"api/file/download/{fileName}" });
        }

<<<<<<< Updated upstream
=======


>>>>>>> Stashed changes
        [HttpDelete("delete/{fileName}")]
        public IActionResult DeleteFile(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return BadRequest("Dosya adı belirtilmemiş");
            }
            var decodeName = Uri.UnescapeDataString(fileName);

            var name = Path.GetFileName(decodeName);
            var rootPath = _webHostEnvironment.WebRootPath;
            var filePath = Path.Combine(rootPath, "img", name);


            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("Dosya bulunamadı");
            }

            try
            {
                System.IO.File.Delete(filePath);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Dosya silinirken bir hata oluştu: {ex.Message}");
            }

            return Ok("Dosya başarıyla silindi");
        }

        [HttpGet("download/{filePath}")]
        public async Task<IActionResult> DownloadFile(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                return BadRequest("Dosya yolu sağlanmadı.");
            }

            var rootPath = _webHostEnvironment.WebRootPath;
            filePath = Path.Combine(rootPath, "img", filePath);

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("Dosya bulunamadı");
            }

            var memory = new MemoryStream();
            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, GetContentType(filePath), Path.GetFileName(filePath));
        }
        

        private string GetContentType(string path)
        {
            var types = new Dictionary<string, string>
            {
                { ".txt", "text/plain" },
                { ".pdf", "application/pdf" },
                { ".doc", "application/vnd.ms-word" },
                { ".docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document" },
                { ".xls", "application/vnd.ms-excel" },
                { ".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" },
                { ".png", "image/png" },
                { ".jpg", "image/jpeg" },
                { ".jpeg", "image/jpeg" },
                { ".gif", "image/gif" },
                { ".csv", "text/csv" }
            };

            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types.ContainsKey(ext) ? types[ext] : "application/octet-stream";
        }
    }
}