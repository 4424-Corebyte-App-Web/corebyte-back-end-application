using System;

namespace Corebyte_platform.orders.Domain.Exceptions
{
    /// <summary>
    /// Exception thrown when a order already exists.
    /// </summary>
    /// 
    public class DuplicateOrderException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the DuplicateOrderException class.
        /// </summary>
        public DuplicateOrderException() : base("The order already exists.")
        {
        }
        /// <summary>
        /// Initializes a new instance of the DuplicateOrderException class.
        /// </summary>
        public DuplicateOrderException(string message) : base(message)
        {
        }
        /// <summary>
        /// Initializes a new instance of the DuplicateOrderException class.
        /// </summary>
        public DuplicateOrderException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
