using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Corebyte_platform.batch_management.Common.Middleware
{
    /// <summary>
    /// Middleware for handling exceptions globally across the application
    /// </summary>
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        /// <summary>
        /// Constructor for ExceptionHandlingMiddleware
        /// </summary>
        /// <param name="next">The next middleware in the pipeline</param>
        /// <param name="logger">Logger for the middleware</param>
        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        /// <summary>
        /// Invoke the middleware
        /// </summary>
        /// <param name="context">The HTTP context</param>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred during request processing");
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            
            var statusCode = HttpStatusCode.InternalServerError;
            var message = "An unexpected error occurred. Please try again later.";

            // Determine appropriate status code based on exception type
            switch (exception)
            {
                case ArgumentNullException:
                case ArgumentException:
                    statusCode = HttpStatusCode.BadRequest;
                    message = exception.Message;
                    _logger.LogWarning(exception, "Bad request: {Message}", exception.Message);
                    break;
                    
                case InvalidOperationException:
                    statusCode = HttpStatusCode.NotFound;
                    message = exception.Message;
                    _logger.LogWarning(exception, "Resource not found: {Message}", exception.Message);
                    break;
                    
                case UnauthorizedAccessException:
                    statusCode = HttpStatusCode.Unauthorized;
                    message = "You are not authorized to perform this action.";
                    _logger.LogWarning(exception, "Unauthorized access attempt: {Message}", exception.Message);
                    break;
                    
                default:
                    _logger.LogError(exception, "Internal server error: {Message}", exception.Message);
                    break;
            }

            context.Response.StatusCode = (int)statusCode;

            var errorResponse = new
            {
                status = (int)statusCode,
                error = statusCode.ToString(),
                message
            };

            var jsonResponse = JsonSerializer.Serialize(errorResponse);
            await context.Response.WriteAsync(jsonResponse);
        }
    }
}

