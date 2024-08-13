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
        Task<List<ServiceDTO>> ListAsyncService();
        Task<Result<ServiceDTO>> AddServiceAsync(ServiceDTO serviceDTO);
        Task<Result<ServiceDTO>> EditServiceAsync(ServiceDTO serviceDTO,long id);
        Task<Result> DeleteServiceAsync(long id);
        Task<List<EducationsDTO>> ListAsyncEducation();
        Task<Result<EducationsDTO>> AddAsyncEducation(EducationsDTO educationsDTO);
        Task<Result<EducationsDTO>> EditAsyncEducation(EducationsDTO educationsDTO, long id);
        Task<Result> DeleteAsyncEducation(long id);
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
            var response = await Client.GetAsync("api/v2/aboutme/list");
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
        public async Task<List<ServiceDTO>> ListAsyncService()
        {
            var response = await Client.GetAsync("api/Services/List");
            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException("User request was not successful");
            }
            var responsObj = await response.Content.ReadFromJsonAsync<List<ServiceDTO>>() ?? throw new InvalidOperationException();

            return Result.Success(responsObj);
        }
        public async Task<Result<ServiceDTO>> AddServiceAsync(ServiceDTO serviceDTO)
        {
            var response = await Client.PostAsJsonAsync("api/Services/add", serviceDTO);
            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException("User request was not successful");
            }
            var responsObj = await response.Content.ReadFromJsonAsync<Result<ServiceDTO>>() ?? throw new InvalidOperationException();

            return Result.Success();
        }
        public async Task<Result<ServiceDTO>> EditServiceAsync(ServiceDTO serviceDTO, long id)
        {
            var response = await Client.PostAsJsonAsync($"api/Services/edit/{id}", serviceDTO);

            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException("User request was not successful");
            }
            var responsObj = await response.Content.ReadFromJsonAsync<Result<ServiceDTO>>() ?? throw new InvalidOperationException();

            return Result.Success();
        }
        public async Task<Result> DeleteServiceAsync(long id)
        {
            var response = await Client.DeleteAsync($"api/Services/delete/{id}");

            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException("User request was not successful");
            }
            return Result.Success();
        }

        public async Task<List<EducationsDTO>> ListAsyncEducation()
        {
            var response = await Client.GetAsync("api/Educations/List");
            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException("User request was not successful");
            }
            var responsObj = await response.Content.ReadFromJsonAsync<List<EducationsDTO>>() ?? throw new InvalidOperationException();

            return Result.Success(responsObj);
        }
        public async Task<Result<EducationsDTO>> AddAsyncEducation(EducationsDTO educationsDTO)
        {
            var response = await Client.PostAsJsonAsync("api/Educations/add", educationsDTO);
            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException("User request was not successful");
            }
            var responsObj = await response.Content.ReadFromJsonAsync<Result<EducationsDTO>>() ?? throw new InvalidOperationException();

            return Result.Success();
        }
        public async Task<Result<EducationsDTO>> EditAsyncEducation(EducationsDTO educationsDTO, long id)
        {
            var response = await Client.PostAsJsonAsync($"api/Educations/edit/{id}", educationsDTO);

            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException("User request was not successful");
            }
            var responsObj = await response.Content.ReadFromJsonAsync<Result<EducationsDTO>>() ?? throw new InvalidOperationException();

            return Result.Success();
        }
        public async Task<Result> DeleteAsyncEducation(long id)
        {
            var response = await Client.DeleteAsync($"api/Educations/delete/{id}");

            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException("User request was not successful");
            }
            return Result.Success();
        }
    }
}
