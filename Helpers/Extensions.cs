using System.Security.Claims;
using ToggleBuddy.API.Middlewares;

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

        public static void UseCustomExceptionHandler(this IApplicationBuilder app) =>
            app.UseMiddleware<ExceptionHandlingMiddleware>();
    }
}
