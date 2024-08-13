using AutoMapper;
using PortfolyoApp.Business.DTOs;
using PortfolyoApp.Data.Entities;

namespace PortfolyoApp.Auth.Api
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AboutMeEntity, AboutMeDTO>().ReverseMap();
        }
    }
}
