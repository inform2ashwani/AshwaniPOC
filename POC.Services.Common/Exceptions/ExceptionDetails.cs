namespace POC.Services.Common.Exceptions
{
    /// <summary>
    /// This holds the formatted exception details
    /// </summary>
    public class ExceptionDetails
    {
        public int StatusCode { get; set; }

        public string? ErrorMessage { get; set; }

        public string? ReasonPhrase { get; set; }
    }
}
