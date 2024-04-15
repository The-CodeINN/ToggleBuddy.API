using AutoMapper;
using ToggleBuddy.API.Models.Domain;

namespace ToggleBuddy.API.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}
