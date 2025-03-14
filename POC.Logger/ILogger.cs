namespace POC.Logger
{
    /// <summary>
    /// Logger interface
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Log the specified message.
        /// </summary>
        /// <param name="msg">The message.</param>
        void Info(string msg);

        /// <summary>
        /// Debugs the specified message.
        /// </summary>
        /// <param name="msg">The message.</param>
        void Debug(string msg);

        /// <summary>
        /// Errors the specified message.
        /// </summary>
        /// <param name="msg">The message.</param>
        void Error(string msg);

        /// <summary>
        /// Errors the specified exception.
        /// </summary>
        /// <param name="exc">The exception.</param>
        void Error(Exception exc);

        /// <summary>
        /// Errors the specified message.
        /// </summary>
        /// <param name="msg">The message.</param>
        /// <param name="exc">The exception.</param>
        void Error(string msg, Exception exc);

    }
}