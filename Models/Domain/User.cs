using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ToggleBuddy.API.Models.Domain
{
    public class User : IdentityUser
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public User() 
        {
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateUpdateAt()
        {
            UpdatedAt = DateTime.UtcNow;
        }

    }
}
