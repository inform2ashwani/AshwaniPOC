namespace POC.Services.Common.Exceptions
{
    /// <summary>
    /// Task not locked for complete exception
    /// </summary>
    public class TaskNotLockedForCompleteException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TaskNotLockedForCompleteException"/> class.
        /// </summary>
        /// <param name="msg">The message.</param>
        public TaskNotLockedForCompleteException(string msg) : base(msg)
        {
        }
    }
}
