using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ToggleBuddy.API.Models.Domain;

namespace ToggleBuddy.API.Data
{
    public class ToggleBuddyDbContext : IdentityDbContext
    {
        public ToggleBuddyDbContext(DbContextOptions<ToggleBuddyDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityUserLogin<string>>().HasKey(x => new { x.LoginProvider, x.ProviderKey });
            modelBuilder.Entity<IdentityUserToken<string>>().HasKey(x => new { x.UserId, x.LoginProvider, x.Name });
            modelBuilder.Entity<IdentityUserRole<string>>().HasKey(x => new { x.UserId, x.RoleId });
        }

        public new DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
    }
}
