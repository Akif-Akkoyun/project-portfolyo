using Ardalis.Result;
using PortfolyoApp.Business.DTOs;
using PortfolyoApp.Business.DTOs.Auth;
using PortfolyoApp.Business.Services.Abstract;
using System.Net.Http.Json;


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
            try
            {
                var response = await Client.PostAsJsonAsync("api/v1/auth/UserLogin", loginDTO);

                if (!response.IsSuccessStatusCode)
                {
                    return Result<AuhtTokenDTO>.Error("Login request was not successful");
                }

                var tokenDto = await response.Content.ReadFromJsonAsync<AuhtTokenDTO>();
                if (tokenDto is null)
                {
                    return Result<AuhtTokenDTO>.Unavailable();
                }

                return Result.Success(tokenDto);
            }
            catch (Exception ex)
            {
                return Result<AuhtTokenDTO>.Error("An error occurred during login: " + ex.Message);
            }
        }
        public async Task<Result<AuhtTokenDTO>> UserLoginAsync(LoginDTO loginDTO)
        {
            try
            {
                var response = await Client.PostAsJsonAsync("api/v1/auth/user-login", loginDTO);

                if (!response.IsSuccessStatusCode)
                {
                    return Result<AuhtTokenDTO>.Error("Login request was not successful");
                }

                var tokenDto = await response.Content.ReadFromJsonAsync<AuhtTokenDTO>();
                if (tokenDto is null)
                {
                    return Result<AuhtTokenDTO>.Unavailable();
                }

                return Result.Success(tokenDto);
            }
            catch (Exception ex)
            {
                return Result<AuhtTokenDTO>.Error("An error occurred during login: " + ex.Message);
            }
        }
        public async Task<Result> RegistersAsync(RegisterDTO registerDTO)
        {
            var response = await Client.PostAsJsonAsync("api/v1/auth/register", registerDTO);
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
        public async Task<List<AppUserDTO>> UserListAsync()
        {
            var response = await Client.GetAsync("api/AppUser/list");
            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException("User request was not successful");
            }
            var responsObj = await response.Content.ReadFromJsonAsync<List<AppUserDTO>>() ?? throw new InvalidOperationException();

            return Result.Success(responsObj);
        }
        public async Task<Result<AppUserDTO>> AddUserAsync(AppUserDTO userDTO)
        {
            var response = await Client.PostAsJsonAsync("api/AppUser/add", userDTO);
            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException("User request was not successful");
            }
            var users = await response
                    .Content
                    .ReadFromJsonAsync<Result<AppUserDTO>>()
                    ?? throw new InvalidOperationException("Fail ");
            return Result.Success(users);
        }
        public async Task<Result<AppUserDTO>> EditUserAsync(AppUserDTO userDTO, long id)
        {
            var response = await Client.PostAsJsonAsync($"api/AppUser/edit/{id}", userDTO);
            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException("User request was not successful");
            }
            var users = await response
                    .Content
                    .ReadFromJsonAsync<Result<AppUserDTO>>()
                    ?? throw new InvalidOperationException("User request was not successful");
            return Result.Success();
        }
        public async Task<Result> UserDeleteAsync(long id)
        {
            var response = await Client.DeleteAsync($"api/u1/User/delete/{id}");

            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException("User request was not successful");
            }
            return Result.Success();
        }

    }
}
