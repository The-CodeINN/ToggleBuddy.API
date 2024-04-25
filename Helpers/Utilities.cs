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
                ResponseStatus.NotFound => new NotFoundObjectResult(response),
                _ => new StatusCodeResult(500)
            };
        }
    }
}
