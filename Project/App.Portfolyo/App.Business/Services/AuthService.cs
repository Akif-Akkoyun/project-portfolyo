using Ardalis.Result;
using PortfolyoApp.Business.DTOs;
using PortfolyoApp.Business.DTOs.Auth;
using PortfolyoApp.Business.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace PortfolyoApp.Business.Services
{
    public class AuthService : IAuthService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AuthService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        private HttpClient Client => _httpClientFactory.CreateClient("auth-api");

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

            return Result.Success(tokenDto);
        }
        public async Task<Result> RegistersAsync(RegisterDTO registerDTO)
        {
            var response = await Client.PostAsJsonAsync("api/v1/auth/register",registerDTO);
            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException("Is not succes ");
            }
            var users = await response
                    .Content
                    .ReadFromJsonAsync<Result<RegisterDTO>>()
                    ?? throw new InvalidOperationException("Fail ");
            return Result.Success();
        }
        public async Task<Result<ForgotPasswordDTO>> ForgotPasswordAsync(ForgotPasswordDTO forgotPasswordDTO)
        {
            var response = await Client.PostAsJsonAsync("api/v1/auth/forgot-password", forgotPasswordDTO);
            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException("Mail don't send");
            }

            var repass = await response.Content.ReadFromJsonAsync<ForgotPasswordDTO>();
            if (repass is null)
            {
                return Result<ForgotPasswordDTO>.Unavailable();
            }

            return Result<ForgotPasswordDTO>.Success(repass);
        }
        public async Task<Result<ResetPasswordDTO>> RenewPasswordAsync(ResetPasswordDTO resetPasswordDTO)
        {
            var response = await Client.PostAsJsonAsync("api/v1/auth/renew-password", resetPasswordDTO);
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
        public async Task<Result> LogoutAsync()
        {
            var response = await Client.PostAsync("api/v1/auth/logout", null);
            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException("Logout request was not successful");
            }

            return Result.Success();
        }
        public async Task<List<UserDTO>> UserListAsync()
        {
            var response = await Client.GetAsync("api/v1/auth/UserList");
            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException("User request was not successful");
            }
            var responsObj = await response.Content.ReadFromJsonAsync<List<UserDTO>>() ?? throw new InvalidOperationException();

            return Result.Success(responsObj);
        }
        public Task<Result> Logut()
        {
            throw new NotImplementedException();
        }
    }
}
