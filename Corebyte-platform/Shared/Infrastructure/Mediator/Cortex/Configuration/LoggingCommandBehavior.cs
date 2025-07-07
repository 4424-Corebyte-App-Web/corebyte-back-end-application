using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Corebyte_platform.Shared.Infrastructure.Mediator.Cortex.Configuration
{
    public class LoggingCommandBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<LoggingCommandBehavior<TRequest, TResponse>> _logger;

        public LoggingCommandBehavior(ILogger<LoggingCommandBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            var requestName = typeof(TRequest).Name;
            _logger.LogInformation("Handling {RequestName}", requestName);
            
            try
            {
                var response = await next();
                _logger.LogInformation("Handled {RequestName} successfully", requestName);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error handling {RequestName}", requestName);
                throw;
            }
        }
    }
}
