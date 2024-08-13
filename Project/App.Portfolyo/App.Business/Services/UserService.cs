using Ardalis.Result;
using PortfolyoApp.Business.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using static ServiceStack.Diagnostics.Events;

namespace PortfolyoApp.Business.Services
{
    public interface IUserService
    {
        Task<List<AboutMeDTO>> UpdateAsync(AboutMeDTO aboutMeDTO);
        Task<List<AboutMeDTO>> GetList();
        Task<List<ExperienceDTO>> ListAsyncExp();
        Task<Result<ExperienceDTO>> AddExpAsync(ExperienceDTO experienceDTO);
        Task<Result<ExperienceDTO>> EditExpAsync(ExperienceDTO experienceDTO, long id);
        Task<Result> DeleteExpAsync(long id);
    }
    public class UserService(IHttpClientFactory httpClientFactory) : IUserService
    {
        private System.Net.Http.HttpClient Client => httpClientFactory.CreateClient("data-api");
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
        public async Task<List<ExperienceDTO>> ListAsyncExp()
        {
            var response = await Client.GetAsync("api/Experience/list");
            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException("User request was not successful");
            }
            var responsObj = await response.Content.ReadFromJsonAsync<List<ExperienceDTO>>() ?? throw new InvalidOperationException();

            return Result.Success(responsObj);
        }
        public async Task<Result<ExperienceDTO>> AddExpAsync(ExperienceDTO experienceDTO)
        {
            var response = await Client.PostAsJsonAsync("api/experience/add", experienceDTO);
            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException("User request was not successful");
            }
            var responsObj = await response.Content.ReadFromJsonAsync<Result<ExperienceDTO>>() ?? throw new InvalidOperationException();

            return Result.Success();
        }
        public async Task<Result<ExperienceDTO>> EditExpAsync(ExperienceDTO experienceDTO, long id)
        {
            var response = await Client.PostAsJsonAsync($"api/experience/edit/{id}", experienceDTO);

            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException("User request was not successful");
            }
            var responsObj = await response.Content.ReadFromJsonAsync<Result<ExperienceDTO>>() ?? throw new InvalidOperationException();

            return Result.Success();
        }
        public async Task<Result> DeleteExpAsync(long id)
        {
            var response = await Client.DeleteAsync($"api/experience/delete/{id}");

            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException("User request was not successful");
            }
           

            return Result.Success();
        }
    }
}
