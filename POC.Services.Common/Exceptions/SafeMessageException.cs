namespace POC.Services.Common.Exceptions
{
    /// <summary>
    /// Safe message exception
    /// </summary>
    /// <remarks>Use this class to throw exception with message that can be displayed to end user</remarks>
    public class SafeMessageException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SafeMessageException"/> class.
        /// </summary>
        /// <param name="msg">The message.</param>
        public SafeMessageException(string msg)
            : base(msg)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SafeMessageException"/> class.
        /// </summary>
        /// <param name="msg">The message.</param>
        /// <param name="exc">The exception.</param>
        public SafeMessageException(string msg, Exception exc)
            : base(msg, exc)
        {
        }

        /// <summary>
        /// Gets the unexpected message.
        /// </summary>
        /// <value>
        /// The unexpected message.
        /// </value>
        public static string UnexpectedMessage
        {
            get
            {
                return "Unexpected error. Please try again or contact your administrator";
            }
        }

        /// <summary>
        /// Unexpected exception
        /// </summary>
        /// <returns>Safe message exception instance</returns>
        public static SafeMessageException Unexpected()
        {
            return new SafeMessageException(UnexpectedMessage);
        }
    }
}
