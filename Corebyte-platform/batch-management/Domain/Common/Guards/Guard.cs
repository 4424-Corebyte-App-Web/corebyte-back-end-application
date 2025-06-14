using System;

namespace Corebyte_platform.batch_management.Domain.Common.Guards
{
    /// <summary>
    /// Provides guard methods for validating arguments
    /// </summary>
    public static class Guard
    {
        /// <summary>
        /// Throws ArgumentNullException if the value is null
        /// </summary>
        /// <typeparam name="T">Type of the argument</typeparam>
        /// <param name="value">The argument value to test</param>
        /// <param name="paramName">Name of the parameter</param>
        /// <exception cref="ArgumentNullException">Thrown when value is null</exception>
        public static void ThrowIfNull<T>(T value, string paramName) where T : class
        {
            if (value == null)
            {
                throw new ArgumentNullException(paramName);
            }
        }

        /// <summary>
        /// Throws ArgumentException if the string value is null or empty
        /// </summary>
        /// <param name="value">The string value to test</param>
        /// <param name="paramName">Name of the parameter</param>
        /// <param name="message">Optional custom message</param>
        /// <exception cref="ArgumentException">Thrown when string is null or empty</exception>
        public static void ThrowIfNullOrEmpty(string value, string paramName, string message = null)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException(message ?? $"{paramName} cannot be null or empty", paramName);
            }
        }
    }
}

