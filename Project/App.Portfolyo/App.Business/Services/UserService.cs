using Ardalis.Result;
using PortfolyoApp.Business.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace PortfolyoApp.Business.Services
{
    public interface IUserService
    {
        Task<List<AboutMeDTO>> UpdateAsync(AboutMeDTO aboutMeDTO);
        Task<List<AboutMeDTO>> GetList();
    }
    public class UserService(IHttpClientFactory httpClientFactory) : IUserService
    {
        private HttpClient Client => httpClientFactory.CreateClient("data-api");
        public async Task<List<AboutMeDTO>> UpdateAsync(AboutMeDTO aboutMeDTO)
        {
            var response = await Client.PostAsJsonAsync("api/v2/aboutme/editaboutme", aboutMeDTO);
            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException("User request was not successful");
            }
            var responsObj = await response.Content.ReadFromJsonAsync<List<AboutMeDTO>>() ?? throw new InvalidOperationException();

            return Result.Success(responsObj);
        }
        public async Task<List<AboutMeDTO>> GetList()
        {
            var response = await Client.GetAsync("api/v2/aboutme/get-about");
            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException("User request was not successful");
            }
            var responsObj = await response.Content.ReadFromJsonAsync<List<AboutMeDTO>>() ?? throw new InvalidOperationException();

            return Result.Success(responsObj);
        }
    }
}
