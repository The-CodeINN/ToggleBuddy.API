using Microsoft.AspNetCore.Mvc;

namespace ToggleBuddy.API.Helpers
{

    public static class Utilities
    {
        public static IActionResult HandleApiResponse<T>(ServiceResponse<T> response)
        {
            return response.Status switch
            {
                ResponseStatus.Success => new OkObjectResult(response),
                ResponseStatus.Error => new BadRequestObjectResult(response),
                ResponseStatus.NotFound => new NotFoundObjectResult(response),
                ResponseStatus.Unauthorized => new UnauthorizedObjectResult(response),
                _ => new StatusCodeResult(500)
            };
        }
    }
}
