namespace POC.Common.Enums
{
    /// <summary>
    /// <c>Consts</c> implementation
    /// </summary>
    public class Consts
    {
        /// <summary>
        /// Replication count index name
        /// </summary>
        public static readonly string ReplicationCountIndexName = "ReplicationCount";

        /// <summary>
        /// Box Id index name
        /// </summary>
        public static readonly string BoxIDPropertyName = "BOX-ID-INDEX-NAME";

        /// <summary>
        /// The index name to update on cover page insert action.
        /// </summary>
        public static readonly string EndorseItemIndexName = "EndorseItem";

        /// <summary>
        /// The print item index name
        /// </summary>
        public static readonly string PrintItemIndexName = "PrintItem";

        /// <summary>
        /// The mask character
        /// </summary>
        public static readonly char MaskCharacter = '*';

        /// <summary>
        /// The exception message thrown when not connected to queue
        /// </summary>
        public static readonly string ExceptionNotConnectedToQueue = "You are not connected to any queue. Refresh and try again.";

        /// <summary>
        /// The exception message when  session expired
        /// </summary>
        public static readonly string ExceptionSessionExpired = "Session expired.";

        /// <summary>
        /// The exception message when not connected to task
        /// </summary>
        public static readonly string ExceptionNotConnectedToTask = "Task is not opened. Refresh and try again.";

        /// <summary>
        /// The modulus 11 value. 
        /// </summary>
        public static readonly string Modulus11 = "11";

        /// <summary>
        /// The modulus 10 value
        /// </summary>
        public static readonly string Modulus10 = "10";

        /// <summary>
        /// The modulus 11 subtract.
        /// </summary>
        public static readonly string Modulus11s = "11S";

        /// <summary>
        /// The page size
        /// </summary>
        public static readonly int PageSize = 25;

        /// <summary>
        /// Rplacement for old session timeout
        /// </summary>
        public static readonly int SessionTimeout = 5;
    }
}
