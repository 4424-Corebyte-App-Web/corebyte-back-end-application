namespace Corebyte_platform.history_status.Domain.Exceptions
{
    /// <summary>
    ///     Exception thrown when a record already exists.
    /// </summary>
    public class DuplicateRecordException: Exception
    {
        /// <summary>
        ///     Initializes a new instance of the DuplicateRecordException class.
        /// </summary>
        public DuplicateRecordException() : base("The record already exists.")
        {
        }
        /// <summary>
        ///     Initializes a new instance of the DuplicateRecordException class.
        /// </summary>
        public DuplicateRecordException(string message) : base(message)
        {
        }
        /// <summary>
        ///     Initializes a new instance of the DuplicateRecordException class.
        /// </summary>
        public DuplicateRecordException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}