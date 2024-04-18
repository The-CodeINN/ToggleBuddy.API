using ToggleBuddy.API.Models.Domain;

namespace ToggleBuddy.API.Respositories.Interfaces
{
    public interface IFeatureEnvironment
    {
        Task<FeatureEnvironment> CreateFeatureEnvironmentAsync(FeatureEnvironment featureEnvironment);
        Task<FeatureEnvironment> GetFeatureEnvironmentAsync();
        Task<FeatureEnvironment?> UpdateFeatureEnvironmentStatusAsync(Guid Id, FeatureEnvironment featureEnvironment);
        Task<FeatureEnvironment?> DeleteFeatureEnvironmentAsync(Guid Id);
        Task<Project?> GetFeatureEnvironmentByIdForCurrentFeatureAsync(Guid id, string? featureId);
    }
}