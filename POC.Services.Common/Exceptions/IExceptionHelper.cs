using Microsoft.AspNetCore.Http;

namespace POC.Services.Common.Exceptions
{
    public interface IExceptionHelper
    {
        ExceptionDetails FormatExceptionResponse(Exception exception);

        string ExtractUserNameFromToken(HttpRequest request, string keyName = "");
    }
}
