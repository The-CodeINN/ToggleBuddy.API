using System.Net.Http.Headers;
using ToggleBuddy.API.Models.Domain;

namespace ToggleBuddy.API.Repositories.Interfaces
{
    public interface IFeatureRepository
    {
        public Task<Feature> CreateAsync(Feature feature, Guid projectId);

        public Task<Feature?> ShowAsync(Guid projectId, Guid featureId);

        public Task<Feature?> UpdateAsync(Feature feature, Guid projectId, Guid featureId);

        public Task<ICollection<Feature>> GetAllAsync(Guid projectId);

        public Task<Feature?> DeleteAsync(Guid projectId, Guid featureId);
    }
}
