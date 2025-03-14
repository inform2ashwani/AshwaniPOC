namespace POC.Services.Common.Exceptions
{
    public class ApiException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApiException"/> class.
        /// </summary>
        /// <param name="msg">The message.</param>
        public ApiException(string msg) : base(msg)
        {
        }
    }
}
