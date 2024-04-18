using AutoMapper.Features;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using ToggleBuddy.API.Data;
using ToggleBuddy.API.Models.Domain;
using ToggleBuddy.API.Respositories.Interfaces;

namespace ToggleBuddy.API.Respositories.Implementations
{
    public class FeatureRepository(ToggleBuddyDbContext dbContext) : IFeatureRepository
    {
       
        public async Task<Feature> CreateAsync(Feature feature)
        {
            await dbContext.Features.AddAsync(feature);
            await dbContext.SaveChangesAsync();
            return feature;
        }

        public async Task<Feature> DeleteAsync(Project project, Guid id)
        {
            var delectFeature = await dbContext.Features.FirstOrDefaultAsync(p => p.Id == id && p.Project.Id==project.Id);
           if(delectFeature == null) 
            {
                return null;
            }

           dbContext.Features.Remove(delectFeature);
           await dbContext.SaveChangesAsync();
            return delectFeature;

        }

        public Task<ICollection<Feature>> GetAllAsync(Project projet)
        {
            throw new NotImplementedException();
        }

        public async Task<Feature> ShowAsync(Project project, Guid id)
        {
            var feature = await dbContext.Features.FirstOrDefaultAsync(x => x.Id == id && x.Project.Id==project.Id);
            if(feature == null)
            {
                return null;
            }
           
            return feature;
        }

        public async Task<Feature> UpdateAsync(Project project, Guid id, Feature feature)
        {
            var updateFeature = await dbContext.Features.FirstOrDefaultAsync(x => x.Id == id && x.Project.Id == project.Id);
            if(updateFeature == null) 

                return null;

            updateFeature.Name = feature.Name;
            updateFeature.Description = feature.Description;
            updateFeature.ExpirationDate = feature.ExpirationDate;
            
            await dbContext.SaveChangesAsync();
            return updateFeature;

            

        }
    }
}
