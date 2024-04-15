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
        public async Task<User?> GetUserByIdAsync(string id)
        {
            return await dbContext.Users.FindAsync(id);
        }
    }
}
