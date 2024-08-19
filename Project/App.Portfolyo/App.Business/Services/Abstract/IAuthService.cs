using Ardalis.Result;
using Microsoft.EntityFrameworkCore.Storage;
using PortfolyoApp.Business.DTOs;
using PortfolyoApp.Business.DTOs.Auth;

namespace PortfolyoApp.Business.Services.Abstract
{
    public interface IAuthService
    {
        Task<Result<AuhtTokenDTO>> LoginAsync(LoginDTO loginDTO);
        Task<Result> RegistersAsync(RegisterDTO registerDTO);
        Task<Result<ForgotPasswordDTO>> ForgotPasswordAsync(ForgotPasswordDTO forgotPasswordDTO);
        Task<Result<ResetPasswordDTO>> RenewPasswordAsync(ResetPasswordDTO resetPasswordDTO);
        Task<List<AppUserDTO>> UserListAsync();
        Task<Result<AppUserDTO>> AddUserAsync(AppUserDTO userDTO);
        Task<Result<AppUserDTO>> EditUserAsync(AppUserDTO userDTO,long id);
        Task<Result> UserDeleteAsync(long id);
    }
}
