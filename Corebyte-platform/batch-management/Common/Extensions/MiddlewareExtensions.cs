using Microsoft.AspNetCore.Builder;
using Corebyte_platform.batch_management.Common.Middleware;

namespace Corebyte_platform.batch_management.Common.Extensions
{
    /// <summary>
    /// Extension methods for middleware registration
    /// </summary>
    public static class MiddlewareExtensions
    {
        /// <summary>
        /// Adds global exception handling middleware to the application pipeline
        /// </summary>
        /// <param name="app">The application builder</param>
        /// <returns>The application builder for chaining</returns>
        public static IApplicationBuilder UseGlobalExceptionHandling(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}

