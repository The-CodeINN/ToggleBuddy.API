using System.Security.Claims;
using ToggleBuddy.API.Data;
using ToggleBuddy.API.Models.Domain;
using ToggleBuddy.API.Respositories.Interfaces;

namespace ToggleBuddy.API.Respositories.Implementations
{
    public class UserRepsitory : IUser
    {
        private readonly ToggleBuddyDbContext dbContext;

        public UserRepsitory(ToggleBuddyDbContext dbContext)
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
