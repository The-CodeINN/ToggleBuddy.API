using System;
using System.Threading.Tasks;

namespace ToggleBuddy.API.Services.Interfaces
{
    public interface IFeatureServices
    {
        public Task<FeatureResponseDto> CreateFeatureAsync(Guid projectId, FeatureRequestDto featureRequestDto, ClaimsPrincipal user);

        public Task<FeatureResponseDto> ShowFeatureAsync(Project project, Guid id);

        public Task<FeatureResponseDto> UpdateFeatureAsync(Project project,Guid id, UpdateFeatureRequestDto updateFeatureRequestDto);

        public Task<FeatureResponseDto> DeleteFeatureAsync(Project project, Guid id)

       
    }
}

