using System.Security.Claims;

namespace ToggleBuddy.API.Helpers
{
    public static class Extensions
    {
        public static string GetLoggedInUserId(this ClaimsPrincipal claims)
        {
            var userId = claims.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
            if (string.IsNullOrEmpty(userId))
                throw new Exception("Session expired. Please logout and login again");
            return userId;
        }
    }
}
