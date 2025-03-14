using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using POC.Services.Common.Exceptions;

namespace POC.Services.Commands.API.Filters
{
    public class ApiExceptionHandlingAttribute : ExceptionFilterAttribute
    {
        /// <summary>
        /// Logging Service
        /// </summary>
       // private readonly Logger.ILogger _logger;

        /// <summary>
        /// Exception helper
        /// </summary>
        private readonly IExceptionHelper _exceptionHelper;

        /// <summary>
        /// DI
        /// </summary>
        /// <param name="logger"></param>
        public ApiExceptionHandlingAttribute(//Logger.ILogger logger,
                                             IExceptionHelper exceptionHelper)
        {
            //_logger = logger;
            _exceptionHelper = exceptionHelper;
        }

        /// <summary>
        /// This method will handle the exceptions caused in APIs
        /// </summary>
        /// <param name="context"></param>
        public override void OnException(ExceptionContext context)
        {
            string message = context.Exception.Message;
            try
            {
                string url = context.HttpContext.Request.GetDisplayUrl();
                if (!string.IsNullOrEmpty(url))
                    message += $" {Environment.NewLine} URL: {url}";
            }
            catch
            {
                message += $" {Environment.NewLine} URL: Error in fetching the url";
            }
            try
            {
                string user = _exceptionHelper.ExtractUserNameFromToken(context.HttpContext.Request);
                if (!string.IsNullOrEmpty(user))
                    message += $" {Environment.NewLine} User: {user}";
            }
            catch
            {
                message += $" {Environment.NewLine} User: Error in fetching the user";
            }

            //_logger.Error(message, context.Exception);

            //Formatting the exception for userfriendly response          
            var exception = _exceptionHelper.FormatExceptionResponse(context.Exception);
            context.Result = new ObjectResult(exception) { StatusCode = exception.StatusCode };

        }
    }
}
