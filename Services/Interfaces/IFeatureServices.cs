// using System;
// using System.Threading.Tasks;
// using ToggleBuddy.API.Models.Domain;
// using ToggleBuddy.API.Models.DTOs.RequestDTOs;
// using ToggleBuddy.API.Models.DTOs.ResponseDTOs;

// namespace ToggleBuddy.API.Services.Interfaces
// {
//     public interface IFeatureServices
//     {
//         public Task<Feature> CreateFeatureAsync(Guid projectId, FeatureRequestDto featureRequestDto);

//         public Task<Feature> ShowFeatureAsync(Project project, Guid id);

//         public Task<Feature> UpdateFeatureAsync(Project project,Guid id, UpdateFeatureRequestDto updateFeatureRequestDto);

//         public Task<Feature> DeleteFeature(Project project, Guid id);

       
//     }
// }

// IFeatureServices.cs
using System;
using System.Threading.Tasks;
using ToggleBuddy.API.Models.Domain;
using ToggleBuddy.API.Models.DTOs.RequestDTOs;
using ToggleBuddy.API.Models.DTOs.ResponseDTOs;

namespace ToggleBuddy.API.Services.Interfaces
{
    public interface IFeatureServices
    {
        Task<Feature> CreateFeatureAsync(Project project,Guid id, FeatureRequestDto featureRequestDto);

        Task<Feature> ShowFeatureAsync(Project project, Guid id);

        Task<Feature> UpdateFeatureAsync(Project project, Guid id, UpdateFeatureRequestDto updateFeatureRequestDto);

        Task<Feature> DeleteFeature(Project project, Guid id);
    }
}
