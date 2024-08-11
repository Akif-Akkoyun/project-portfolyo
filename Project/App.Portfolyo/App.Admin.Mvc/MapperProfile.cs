using AutoMapper;
using PortfolyoApp.Admin.Mvc.Models;
using PortfolyoApp.Business.DTOs;
using PortfolyoApp.Business.DTOs.Auth;
using ServiceStack.Auth;

namespace PortfolyoApp.Admin.Mvc
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<UserViewModel, UserDTO>()
                .ReverseMap();
            CreateMap<AboutMeViewModel, AboutMeDTO>()
                .ReverseMap();
            CreateMap<LoginViewModel, LoginDTO>()
                .ReverseMap();
        }
    }
}
