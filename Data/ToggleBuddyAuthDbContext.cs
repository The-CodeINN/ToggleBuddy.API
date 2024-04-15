using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ToggleBuddy.API.Data
{
    public class ToggleBuddyAuthDbContext : IdentityDbContext
    {
        public ToggleBuddyAuthDbContext(DbContextOptions<ToggleBuddyAuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerRoleId = "a8a8be1b-497a-4a76-a999-155492de85ab";
            var writerRoleId = "33c995f4-1032-457f-a98b-2e2e2ef255cb";
            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = readerRoleId,
                    ConcurrencyStamp =readerRoleId,
                    Name = "Reader",
                    NormalizedName = "Reader".ToUpper()
                },


                new IdentityRole
                {
                    Id = writerRoleId,
                    ConcurrencyStamp = writerRoleId,
                    Name = "Writer",
                    NormalizedName = "Writer".ToUpper()
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);



        }
    }
}
