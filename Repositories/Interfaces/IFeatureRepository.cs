using System.Net.Http.Headers;
using ToggleBuddy.API.Models.Domain;

namespace ToggleBuddy.API.Respositories.Interfaces
{
    public interface IFeatureRepository
    {
        public Task<Feature> CreateAsync(Feature feature);

        public Task<Feature> ShowAsync(Project project, Guid id);

        public Task<Feature> UpdateAsync(Project project, Guid id, Feature feature);

        public Task<ICollection<Feature>> GetAllAsync(Project projet);

        public Task<Feature> DeleteAsync(Project project, Guid id);
    }
}
