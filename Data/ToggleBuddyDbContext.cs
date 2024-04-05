using Microsoft.EntityFrameworkCore;

namespace ToggleBuddy.API.Data
{
    public class ToggleBuddyDbContext: DbContext
    {
        public ToggleBuddyDbContext(DbContextOptions<ToggleBuddyDbContext> dbContextOptions) : base(dbContextOptions) 
        {
            
        }

    }
}
