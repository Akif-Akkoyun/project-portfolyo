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
        Task<List<ProjectDTO>> ListAsyncProject();
        Task<ProjectDTO> GetProjectAsync(long id);
        Task<Result<ProjectDTO>> AddAsyncProject(ProjectDTO projectDto);
        Task<Result<ProjectDTO>> EditAsyncProject(ProjectDTO projectDto, long id);
        Task<Result> DeleteAsyncProject(long id);
        Task<List<ContactDTO>> ListAsyncContact();
        Task<Result<ContactDTO>> AddAsyncContact(ContactDTO contactDTO);
        Task<Result> DeleteContact(long id);
        Task<List<BlogPostDTO>> ListAsyncBlog();
        Task<Result<BlogPostDTO>> AddAsyncBlog(BlogPostDTO blogtDto);
        Task<BlogPostDTO> DetailAsyncBlog(long id);
        Task<Result<BlogPostDTO>> EditAsyncBlog(BlogPostDTO blogtDto, long id);
        Task<Result> DeleteAsyncBlog(long id);
        Task<Result<CommentDTO>> AddCommentAsync(CommentDTO comment, long id);
    }
    public class UserService(IHttpClientFactory httpClientFactory) : IUserService
    {
<<<<<<< Updated upstream
        private System.Net.Http.HttpClient DataApiClient => httpClientFactory.CreateClient("data-api");
        private System.Net.Http.HttpClient FileApiClient => httpClientFactory.CreateClient("file-api");
        public async Task<List<AboutMeDTO>> UpdateAsync(AboutMeDTO aboutMeDTO)
=======
        private HttpClient DataApiClient => httpClientFactory.CreateClient("data-api");
        private HttpClient FileApiClient => httpClientFactory.CreateClient("file-api");
        
        public async Task<List<SlidersDTO>> GetSliderListAsync()
>>>>>>> Stashed changes
        {
            var response = await DataApiClient.PostAsJsonAsync("api/v2/aboutme/editaboutme", aboutMeDTO);
            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException("User request was not successful");
            }
            var responsObj = await response.Content.ReadFromJsonAsync<List<AboutMeDTO>>() ?? throw new InvalidOperationException();

            return Result.Success(responsObj);
        }
<<<<<<< Updated upstream
=======
        public async Task<SlidersDTO> SliderGetIdAsync(long id)
        {
            var response = await DataApiClient.GetAsync($"api/slider/Get/{id}");

            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException("User request was not successful");
            }
            var responsObj = await response.Content.ReadFromJsonAsync<SlidersDTO>() ?? throw new InvalidOperationException();

            return responsObj ?? throw new InvalidOperationException("No blog post data found");
        }
        public async Task<Result<SlidersDTO>> EditSliderAsync(SlidersDTO slidersDTO, long id)
        {
            slidersDTO.ImgUrl1 = $"{FileApiClient.BaseAddress}{slidersDTO.ImgUrl1}";
            slidersDTO.ImgUrl2 = $"{FileApiClient.BaseAddress}{slidersDTO.ImgUrl2}";
            var response = await DataApiClient.PutAsJsonAsync($"api/slider/edit/{id}", slidersDTO);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new InvalidOperationException($"User request was not successful: {response.StatusCode} - {errorContent}");
            }
            var responsObj = await response.Content.ReadFromJsonAsync<Result<SlidersDTO>>() ?? throw new InvalidOperationException();

            return Result.Success();
        }
        public async Task<Result<AboutMeDTO>> EditAboutMeAsync(AboutMeDTO aboutMeDTO, long id)
        {
            aboutMeDTO.ImageUrl1 = $"{FileApiClient.BaseAddress}{aboutMeDTO.ImageUrl1}";
            aboutMeDTO.CvUrl = $"{FileApiClient.BaseAddress}{aboutMeDTO.CvUrl}";
            var response = await DataApiClient.PutAsJsonAsync($"api/v2/aboutme/edit/{id}", aboutMeDTO);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new InvalidOperationException($"User request was not successful: {response.StatusCode} - {errorContent}");
            }
            var responsObj = await response.Content.ReadFromJsonAsync<Result<AboutMeDTO>>() ?? throw new InvalidOperationException();

            return Result.Success();
        }
>>>>>>> Stashed changes
        public async Task<List<AboutMeDTO>> GetList()
        {
            var response = await DataApiClient.GetAsync("api/v2/aboutme/list");
            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException("User request was not successful");
            }
            var responsObj = await response.Content.ReadFromJsonAsync<List<AboutMeDTO>>() ?? throw new InvalidOperationException();

            return Result.Success(responsObj);
        }
        public async Task<List<ExperienceDTO>> ListAsyncExp()
        {
            var response = await DataApiClient.GetAsync("api/Experience/list");
            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException("User request was not successful");
            }
            var responsObj = await response.Content.ReadFromJsonAsync<List<ExperienceDTO>>() ?? throw new InvalidOperationException();

            return Result.Success(responsObj);
        }
        public async Task<Result<ExperienceDTO>> AddExpAsync(ExperienceDTO experienceDTO)
        {
            var response = await DataApiClient.PostAsJsonAsync("api/experience/add", experienceDTO);
            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException("User request was not successful");
            }
            var responsObj = await response.Content.ReadFromJsonAsync<Result<ExperienceDTO>>() ?? throw new InvalidOperationException();

            return Result.Success();
        }
        public async Task<Result<ExperienceDTO>> EditExpAsync(ExperienceDTO experienceDTO, long id)
        {
            var response = await DataApiClient.PostAsJsonAsync($"api/experience/edit/{id}", experienceDTO);

            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException("User request was not successful");
            }
            var responsObj = await response.Content.ReadFromJsonAsync<Result<ExperienceDTO>>() ?? throw new InvalidOperationException();

            return Result.Success();
        }
        public async Task<Result> DeleteExpAsync(long id)
        {
            var response = await DataApiClient.DeleteAsync($"api/experience/delete/{id}");

            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException("User request was not successful");
            }
            return Result.Success();
        }
        public async Task<List<ServiceDTO>> ListAsyncService()
        {
            var response = await DataApiClient.GetAsync("api/Services/List");
            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException("User request was not successful");
            }
            var responsObj = await response.Content.ReadFromJsonAsync<List<ServiceDTO>>() ?? throw new InvalidOperationException();

            return Result.Success(responsObj);
        }
        public async Task<Result<ServiceDTO>> AddServiceAsync(ServiceDTO serviceDTO)
        {
            var response = await DataApiClient.PostAsJsonAsync("api/Services/add", serviceDTO);
            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException("User request was not successful");
            }
            var responsObj = await response.Content.ReadFromJsonAsync<Result<ServiceDTO>>() ?? throw new InvalidOperationException();

            return Result.Success();
        }
        public async Task<Result<ServiceDTO>> EditServiceAsync(ServiceDTO serviceDTO, long id)
        {
            var response = await DataApiClient.PostAsJsonAsync($"api/Services/edit/{id}", serviceDTO);

            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException("User request was not successful");
            }
            var responsObj = await response.Content.ReadFromJsonAsync<Result<ServiceDTO>>() ?? throw new InvalidOperationException();

            return Result.Success();
        }
        public async Task<Result> DeleteServiceAsync(long id)
        {
            var response = await DataApiClient.DeleteAsync($"api/Services/delete/{id}");

            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException("User request was not successful");
            }
            return Result.Success();
        }

        public async Task<List<EducationsDTO>> ListAsyncEducation()
        {
            var response = await DataApiClient.GetAsync("api/Educations/List");
            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException("User request was not successful");
            }
            var responsObj = await response.Content.ReadFromJsonAsync<List<EducationsDTO>>() ?? throw new InvalidOperationException();

            return Result.Success(responsObj);
        }
        public async Task<Result<EducationsDTO>> AddAsyncEducation(EducationsDTO educationsDTO)
        {
            var response = await DataApiClient.PostAsJsonAsync("api/Educations/add", educationsDTO);
            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException("User request was not successful");
            }
            var responsObj = await response.Content.ReadFromJsonAsync<Result<EducationsDTO>>() ?? throw new InvalidOperationException();

            return Result.Success();
        }
        public async Task<Result<EducationsDTO>> EditAsyncEducation(EducationsDTO educationsDTO, long id)
        {
            var response = await DataApiClient.PostAsJsonAsync($"api/Educations/edit/{id}", educationsDTO);

            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException("User request was not successful");
            }
            var responsObj = await response.Content.ReadFromJsonAsync<Result<EducationsDTO>>() ?? throw new InvalidOperationException();

            return Result.Success();
        }
        public async Task<Result> DeleteAsyncEducation(long id)
        {
            var response = await DataApiClient.DeleteAsync($"api/Educations/delete/{id}");

            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException("User request was not successful");
            }
            return Result.Success();
        }
        public async Task<List<ProjectDTO>> ListAsyncProject()
        {
            var response = await DataApiClient.GetAsync("api/project/List");
            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException("User request was not successful");
            }
            var responsObj = await response.Content.ReadFromJsonAsync<List<ProjectDTO>>() ?? throw new InvalidOperationException();

            return Result.Success(responsObj);
        }
        public async Task<ProjectDTO> GetProjectAsync(long id)
        {
            var response = await DataApiClient.GetAsync($"api/Project/get/{id}");

            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException("User request was not successful");
            }
            var responsObj = await response.Content.ReadFromJsonAsync<ProjectDTO>() ?? throw new InvalidOperationException();

            return Result.Success(responsObj);
        }
        public async Task<Result<ProjectDTO>> AddAsyncProject(ProjectDTO projectDto)
        {
            try
            {
                projectDto.ImageUrl = $"{FileApiClient.BaseAddress}{projectDto.ImageUrl}";
                var response = await DataApiClient.PostAsJsonAsync("api/Project/add", projectDto);
                if (!response.IsSuccessStatusCode)
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    throw new InvalidOperationException($"Request was not successful: {errorMessage}");
                }

                var responsObj = await response.Content.ReadFromJsonAsync<Result<ProjectDTO>>() ?? throw new InvalidOperationException("Failed to deserialize response");
                return Result.Success(responsObj);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred in AddAsyncProject", ex);
            }
        }
        public async Task<Result<ProjectDTO>> EditAsyncProject(ProjectDTO projectDto, long id)
        {
            projectDto.ImageUrl = $"{FileApiClient.BaseAddress}{projectDto.ImageUrl}";
            var response = await DataApiClient.PutAsJsonAsync($"api/Project/edit/{id}", projectDto);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new InvalidOperationException($"User request was not successful: {response.StatusCode} - {errorContent}");
            }
            var responsObj = await response.Content.ReadFromJsonAsync<Result<ProjectDTO>>() ?? throw new InvalidOperationException();

            return Result.Success();
        }
        public async Task<Result> DeleteAsyncProject(long id)
        {
            var response = await DataApiClient.DeleteAsync($"api/Project/delete/{id}");

            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException("User request was not successful");
            }
            return Result.Success();
        }
        public async Task<List<ContactDTO>> ListAsyncContact()
        {
            var response = await DataApiClient.GetAsync("api/Contact/List");
            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException("User request was not successful");
            }
            var responsObj = await response.Content.ReadFromJsonAsync<List<ContactDTO>>() ?? throw new InvalidOperationException();

            return Result.Success(responsObj);
        }
        public async Task<Result<ContactDTO>> AddAsyncContact(ContactDTO contactDTO)
        {
            var response = await DataApiClient.PostAsJsonAsync("api/Contact/add", contactDTO);
            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException("User request was not successful");
            }
            var responsObj = await response.Content.ReadFromJsonAsync<Result<ContactDTO>>() ?? throw new InvalidOperationException();

            return Result.Success();
        }
        public async Task<Result> DeleteContact(long id)
        {
            var response = await DataApiClient.DeleteAsync($"api/Contact/delete/{id}");

            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException("User request was not successful");
            }
            return Result.Success();
        }
        public async Task<List<BlogPostDTO>> ListAsyncBlog()
        {
            var response = await DataApiClient.GetAsync("api/blogpost/list");
            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException("User request was not successful");
            }
            var responsObj = await response.Content.ReadFromJsonAsync<List<BlogPostDTO>>() ?? throw new InvalidOperationException();

            return Result.Success(responsObj);
        }
        public async Task<Result<BlogPostDTO>> AddAsyncBlog(BlogPostDTO blogDto)
        {
            try
            {
                blogDto.ImageUrl = $"{FileApiClient.BaseAddress}{blogDto.ImageUrl}";
                var response = await DataApiClient.PostAsJsonAsync("api/BlogPost/add", blogDto);
                if (!response.IsSuccessStatusCode)
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    throw new InvalidOperationException($"Request was not successful: {errorMessage}");
                }

                var responsObj = await response.Content.ReadFromJsonAsync<Result<BlogPostDTO>>() ?? throw new InvalidOperationException("Failed to deserialize response");
                return Result.Success(responsObj);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred in AddAsyncProject", ex);
            }
        }
        public async Task<BlogPostDTO> DetailAsyncBlog(long id)
        {
            var response = await DataApiClient.GetAsync($"api/BlogPost/detail/{id}");

            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException("User request was not successful");
            }
            var responsObj = await response.Content.ReadFromJsonAsync<BlogPostDTO>() ?? throw new InvalidOperationException();

            return responsObj ?? throw new InvalidOperationException("No blog post data found");
        }
        public async Task<Result<BlogPostDTO>> EditAsyncBlog(BlogPostDTO blogtDto, long id)
        {
            var response = await DataApiClient.PostAsJsonAsync($"api/BlogPost/edit/{id}", blogtDto);

            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException("User request was not successful");
            }
            var responsObj = await response.Content.ReadFromJsonAsync<Result<BlogPostDTO>>() ?? throw new InvalidOperationException();

            return Result.Success();
        }
        public async Task<Result> DeleteAsyncBlog(long id)
        {
            var response = await DataApiClient.DeleteAsync($"api/BlogPost/delete/{id}");

            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException("User request was not successful");
            }
            return Result.Success();
        }
        public async Task<Result<CommentDTO>> AddCommentAsync(CommentDTO comment,long id)
        {
            try
            {
                var response = await DataApiClient.PostAsJsonAsync($"api/BlogPost/add-comment{id}", comment);
                if (!response.IsSuccessStatusCode)
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    throw new InvalidOperationException($"Request was not successful: {errorMessage}");
                }

                var responsObj = await response.Content.ReadFromJsonAsync<Result<CommentDTO>>() ?? throw new InvalidOperationException("Failed");
                return Result.Success(responsObj);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error", ex);
            }
        }
    }
}
