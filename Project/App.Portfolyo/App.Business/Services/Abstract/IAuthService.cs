using Ardalis.Result;
using PortfolyoApp.Business.DTOs.Auth;

namespace PortfolyoApp.Business.Services.Abstract
{
    public interface IAuthService
    {
        Task<Result<AuhtTokenDTO>> LoginAsync(LoginDTO loginDTO);
        Task<Result> RegistersAsync(RegisterDTO registerDTO);
        Task<Result<ForgotPasswordDTO>> ForgotPasswordAsync(ForgotPasswordDTO forgotPasswordDTO);
        Task<Result<ResetPasswordDTO>> RenewPasswordAsync(ResetPasswordDTO resetPasswordDTO);
    }
}
