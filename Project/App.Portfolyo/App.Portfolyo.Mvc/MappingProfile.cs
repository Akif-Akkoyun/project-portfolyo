using AutoMapper;
using PortfolyoApp.Business.DTOs.Auth;
using PortfolyoApp.Mvc.Models;
using ServiceStack;

namespace PortfolyoApp.Mvc
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<LoginViewModel, LoginDTO>()
                .ReverseMap();
            CreateMap<RegisterViewModel, RegisterDTO>()
                .ReverseMap();
            CreateMap<ForgotPasswordViewModel, ForgotPasswordDTO>()
                .ReverseMap();
            CreateMap<RenewPasswordViewModel, ResetPasswordDTO>()
                .ReverseMap();
        }
    }
}
