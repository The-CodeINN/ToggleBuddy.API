using System.Security.Claims;
using ToggleBuddy.API.Models.Domain;

namespace ToggleBuddy.API.Respositories.Interfaces
{
    public interface IUser
    {
            Task<User?> GetCurrentUserAsync(ClaimsPrincipal user);

    }

}

