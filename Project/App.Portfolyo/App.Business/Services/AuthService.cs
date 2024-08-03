using Ardalis.Result;
using PortfolyoApp.Business.DTOs;
using PortfolyoApp.Business.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace PortfolyoApp.Business.Services
{
    public class AuthService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AuthService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        private HttpClient Client => _httpClientFactory.CreateClient("AuthApi");
        public async Task<Result<TokenDTO>> GetLoginRequestAsync(LoginDTO loginDTO)
        {
            var response = await Client.PostAsJsonAsync("api/v1/auth/login", loginDTO);
            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException("Login request was not successful");
            }

            var tokenDto = await response.Content.ReadFromJsonAsync<TokenDTO>();
            if (tokenDto is null)
            {
                return Result<TokenDTO>.Unavailable();
            }

            return Result<TokenDTO>.Success(tokenDto);
        }
        public async Task<List<RegisterDTO>> GetUser(RegisterDTO registerDTO)
        {
            var response = await Client.PostAsJsonAsync("api/v1/auth/register",registerDTO);
            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException("Is not succes ");
            }
            var users = await response
                    .Content
                    .ReadFromJsonAsync<List<RegisterDTO>>()
                    ?? throw new InvalidOperationException("Fail ");
            return users;
        }

    }
}
