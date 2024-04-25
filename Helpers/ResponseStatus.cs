namespace ToggleBuddy.API.Helpers
{
    public enum ResponseStatus
    {
        Success,
        Error,
        Processing,
        NotFound,
        Unauthorized,
        BadRequest
    }


    public class ServiceResponse<T>
    {
        public ResponseStatus Status { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Result { get; set; }
    }
}
