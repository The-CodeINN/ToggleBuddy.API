using System.Security.Claims;
using ToggleBuddy.API.Data;
using ToggleBuddy.API.Models.Domain;
using ToggleBuddy.API.Repositories.Interfaces;

namespace ToggleBuddy.API.Repositories.Implementations
{
    public class UserRepository : IUser
    {
        private readonly ToggleBuddyDbContext dbContext;

        public UserRepository(ToggleBuddyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<User?> GetCurrentUserAsync(ClaimsPrincipal user)
        {
            var userId = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
            return await dbContext.Users.FindAsync(userId);
        }
    }
}
