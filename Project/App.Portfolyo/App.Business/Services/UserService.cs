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
    public class UserService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        private HttpClient Client => _httpClientFactory.CreateClient("DataApi");
        public UserService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<List<UserDTO>> GetUser()
        {
            var response = await Client.GetAsync("api/v1/user");
            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException("Is not succes ");
            }
            var users = await response
                    .Content
                    .ReadFromJsonAsync<List<UserDTO>>()
                    ?? throw new InvalidOperationException("Fail ");
            return users;
        }
    }
}
