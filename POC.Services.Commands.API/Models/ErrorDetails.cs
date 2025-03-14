using Newtonsoft.Json;

namespace POC.Services.Commands.API.Models
{
    /// <summary>
    /// ErrorDetails
    /// </summary>
    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public string? ReasonPhrase { get; set; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}
