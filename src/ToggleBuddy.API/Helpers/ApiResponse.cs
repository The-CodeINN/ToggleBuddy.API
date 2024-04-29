namespace ToggleBuddy.API.Helpers
{
    public class ApiResponse<T>
    {
        public ResponseStatus Status { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Result { get; set; }
    }
}
