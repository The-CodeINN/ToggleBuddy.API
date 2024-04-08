using Microsoft.AspNetCore.Identity;

namespace ToggleBuddy.API.Respositories.Interfaces
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
