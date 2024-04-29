using AutoMapper;
using ToggleBuddy.API.Models.Domain;
using ToggleBuddy.API.Models.DTOs;
using ToggleBuddy.API.Models.DTOs.RequestDTOs;
using ToggleBuddy.API.Models.DTOs.ResponseDTOs;

namespace ToggleBuddy.API.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<ProjectRequestDto, Project>().ReverseMap();
            CreateMap<Project, ProjectResponseDto>().ReverseMap();
            CreateMap<Feature, FeatureRequestDto>().ReverseMap();
            CreateMap<Feature, FeatureResponseDto>().ReverseMap();
            CreateMap<Feature, UpdateFeatureResponseDto>().ReverseMap();
            CreateMap<Feature, UpdateFeatureRequestDto>().ReverseMap();
            CreateMap<FeatureEnvironment, FeatureEnvironmentRequestDto>().ReverseMap();
            CreateMap<FeatureEnvironment, FeatureEnvironmentResponseDto>().ReverseMap();
        }
    }
}
