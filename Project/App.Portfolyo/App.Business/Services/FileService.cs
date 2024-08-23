using Ardalis.Result;
using Microsoft.AspNetCore.Http;
using PortfolyoApp.Business.DTOs.File;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace PortfolyoApp.Business.Services
{
    public class FileService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public FileService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        private HttpClient Client => _httpClientFactory.CreateClient("file-api");
        public async Task<Result<string>> UploadFileAsync(IFormFile file)
        {
            using var content = new MultipartFormDataContent();
            using var fileStream = file.OpenReadStream();
            var streamContent = new StreamContent(fileStream);
            streamContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
            content.Add(streamContent, "file", file.FileName);

            HttpResponseMessage response;
            try
            {
                response = await Client.PostAsync("api/file/upload", content);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error during file upload: " + ex.Message);
                return Result.Error("File upload failed.");
            }

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<FileUploadResponse>();
                if (result == null || string.IsNullOrEmpty(result.FilePath))
                {
                    return Result.Error("File upload failed.");
                }
                return Result.Success(result.FilePath);
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine("File upload failed. Status Code: " + response.StatusCode);
                Console.WriteLine("Response Content: " + errorContent);
                return Result.Error("File upload failed: " + errorContent);
            }
        }

    }
}

