using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PortfolyoApp.Business.Services;
using PortfolyoApp.Data.Entities;

namespace PortfolyoApp.File.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly FileService _fileService;
        private readonly string _fileStoragePath;

        public FileController(FileService fileService, IConfiguration configuration)
        {
            _fileService = fileService;
            _fileStoragePath = configuration["FileStoragePath"] ?? throw new InvalidOperationException();
        }
        [HttpGet]
        public async Task<IEnumerable<FileEntity>> Get()
        {
            return await _fileService.GetAllFilesAsync();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var file = await _fileService.GetFileByIdAsync(id);
            if (file == null)
            {
                return NotFound();
            }

            var fileStream = System.IO.File.OpenRead(file.FilePath);
            return File(fileStream, "application/octet-stream", file.FileName);
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile([FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("Dosya yüklenmemiş");
            }

            var filePath = Path.Combine(_fileStoragePath, file.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var newFile = new FileEntity
            {
                FileName = file.FileName,
                FilePath = filePath
            };

            await _fileService.AddFileAsync(newFile);

            return Ok(new { filePath });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _fileService.DeleteFileAsync(id);
            return Ok();
        }
    }
}
