using log4net;

namespace POC.Logger
{
    /// <summary>
    /// Logger class
    /// </summary>
   public class Logger : ILogger
    {
        /// <summary>
        /// The log
        /// </summary>
        private ILog log;

        /// <summary>
        /// Initializes a new instance of the <see cref="Logger"/> class.
        /// </summary>
        public Logger()
        {
            log4net.Config.XmlConfigurator.Configure();
            log = LogManager.GetLogger("DefaultLogger");
            log4net.Config.BasicConfigurator.Configure();
        }

        /// <summary>
        /// Information the specified MSG.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        public void Info(string msg)
        {
            try
            {
                log.Info(msg);
            }
            catch (Exception exc)
            {
                log.Error(exc.Message, exc);
            }
        }

        /// <summary>
        /// Debugs the specified MSG.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        public void Debug(string msg)
        {
            try
            {
                log.Debug(msg);
            }
            catch (Exception exc)
            {
                log.Error(exc.Message, exc);
            }
        }

        /// <summary>
        /// Errors the specified MSG.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        public void Error(string msg)
        {
            log.Error(msg);
        }

        /// <summary>
        /// Errors the specified exception.
        /// </summary>
        /// <param name="exc">The exception.</param>
        public void Error(Exception exc)
        {
            log.Error(exc.Message, exc);
        }

        /// <summary>
        /// Errors the specified message.
        /// </summary>
        /// <param name="msg">The message.</param>
        /// <param name="exc">The exception.</param>
        public void Error(string msg, Exception exc)
        {
            if (msg.Contains("Password="))
            {
                // prevent from logging user password
                log.Error($"Exception has occured but message contains sensitive information - can't log details.");
            }
            else
            {
                log.Error(msg, exc);
            }
        }
    }
}
