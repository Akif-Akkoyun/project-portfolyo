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

        public async Task<Result<AuhtTokenDTO>> LoginAsync(LoginDTO loginDTO)
        {
            var response = await Client.PostAsJsonAsync("api/v1/auth/login", loginDTO);
            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException("Login request was not successful");
            }

            var tokenDto = await response.Content.ReadFromJsonAsync<AuhtTokenDTO>();
            if (tokenDto is null)
            {
                return Result<AuhtTokenDTO>.Unavailable();
            }

            return Result<AuhtTokenDTO>.Success(tokenDto);
        }
        public async Task<List<RegisterDTO>> RegistersAsync(RegisterDTO registerDTO)
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
        public async Task<Result<ForgotPasswordDTO>> ResetPasswordAsync(ForgotPasswordDTO forgotPasswordDTO)
        {
            var response = await Client.PutAsJsonAsync("api/v1/auth/forgot-password", forgotPasswordDTO);
            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException("Register request was not successful");
            }

            var repass = await response.Content.ReadFromJsonAsync<ForgotPasswordDTO>();
            if (repass is null)
            {
                return Result<ForgotPasswordDTO>.Unavailable();
            }

            return Result<ForgotPasswordDTO>.Success(repass);
        }
        public async Task<Result<ResetPasswordDTO>> ResetPasswordAsync(ResetPasswordDTO resetPasswordDTO)
        {
            var response = await Client.PostAsJsonAsync("api/v1/auth/reset-password", resetPasswordDTO);
            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException("Register request was not successful");
            }

            var repass = await response.Content.ReadFromJsonAsync<ResetPasswordDTO>();
            if (repass is null)
            {
                return Result<ResetPasswordDTO>.Unavailable();
            }

            return Result<ResetPasswordDTO>.Success(repass);
        }
    }
}
