using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace POC.Services.Common.Exceptions
{
    public class ExceptionHelper : IExceptionHelper
    {
        /// <summary>
        /// This method will format the exception in order not to expose the exception details to the end user
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        public ExceptionDetails FormatExceptionResponse(Exception exception)
        {
            //Set the reason phrase as per the exception type
            string reasonPhrase = exception switch
            {
                SafeMessageException => exception.Message,
                ApiException => exception.Message,
                TaskNotLockedForCompleteException => "Task was modified since opened. Cannot complete operation",
                _ => "Something went wrong. Please try again or contact the administrator.",
            };
            var exceptionDetails = new ExceptionDetails
            {
                ErrorMessage = exception.Message,
                ReasonPhrase = reasonPhrase,
                StatusCode = (int)HttpStatusCode.InternalServerError,
            };

            return exceptionDetails;
        }

        /// <summary>
        /// This method will get the username from the request token for logging
        /// </summary>
        /// <param name="request"></param>
        /// <param name="keyName"></param>
        /// <returns></returns>
        public string ExtractUserNameFromToken(HttpRequest request, string keyName = "")
        {
            string? token = string.Empty;
            string? userName = string.Empty;
            //Reterive token from the request header- This part will be executed when Web2 error filter is fired in case of exception
            if (!string.IsNullOrWhiteSpace(keyName))
            {
                request.Headers.TryGetValue(keyName, out var _token);
                token = _token;
            }
            //Reterive token from Authorization header- This part will be executed when API error filter is fired in case of exception
            else
            {
                var authorization = request.Headers["Authorization"];
                if (AuthenticationHeaderValue.TryParse(authorization, out var headerValue))
                {
                    token = headerValue?.Parameter;
                }
            }
            if (!string.IsNullOrEmpty(token))
                //Getting the name identifier from the JWT token
                userName = ((Microsoft.IdentityModel.Tokens.SecurityToken?)new JwtSecurityTokenHandler().ReadToken(token)
                    as JwtSecurityToken).Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value;
            return userName;
        }
    } 
}
