using System;

namespace Corebyte_platform.history_status.Domain.Exceptions
{
    /// <summary>
    ///     Exception thrown when a history already exists.
    /// </summary>
    public class DuplicateHistoryException : Exception
    {
        /// <summary>
        ///     Initializes a new instance of the DuplicateHistoryException class.
        /// </summary>
        public DuplicateHistoryException() : base("The history already exists.")
        {
        }

        /// <summary>
        ///     Initializes a new instance of the DuplicateHistoryException class.
        /// </summary>
        public DuplicateHistoryException(string message) : base(message)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the DuplicateHistoryException class.
        /// </summary>
        public DuplicateHistoryException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
