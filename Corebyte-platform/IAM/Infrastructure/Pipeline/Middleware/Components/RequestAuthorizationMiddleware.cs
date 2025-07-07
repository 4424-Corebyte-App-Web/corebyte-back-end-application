using Corebyte_platform.IAM.Domain.Model.Queries;
using Corebyte_platform.IAM.Application.Internal.OutboundServices;
using Corebyte_platform.IAM.Domain.Services;
using Corebyte_platform.IAM.Infrastructure.Pipeline.Middleware.Attributes;
using System.Security.Authentication;


namespace Corebyte_platform.IAM.Infrastructure.Pipeline.Middleware.Components;

/**
 * RequestAuthorizationMiddleware is a custom middleware.
 * This middleware is used to authorize requests.
 * It validates a token is included in the request header and that the token is valid.
 * If the token is valid then it sets the user in HttpContext.Items["User"].
 */
public class RequestAuthorizationMiddleware(RequestDelegate next, ILogger<RequestAuthorizationMiddleware> logger)
{
    private readonly ILogger<RequestAuthorizationMiddleware> _logger = logger;
    /**
     * InvokeAsync is called by the ASP.NET Core runtime.
     * It is used to authorize requests.
     * It validates a token is included in the request header and that the token is valid.
     * If the token is valid then it sets the user in HttpContext.Items["User"].
     */
    public async Task InvokeAsync(
        HttpContext context,
        IUserQueryService userQueryService,
        ITokenService tokenService)
    {
        _logger.LogInformation("Entering InvokeAsync");
        // skip authorization if endpoint is decorated with [AllowAnonymous] attribute
        var allowAnonymous = context.Request.HttpContext.GetEndpoint()!.Metadata
            .Any(m => m.GetType() == typeof(AllowAnonymousAttribute));
        _logger.LogInformation("Allow Anonymous is {AllowAnonymous}", allowAnonymous);
        if (allowAnonymous)
        {
            _logger.LogInformation("Skipping authorization");
            // [AllowAnonymous] attribute is set, so skip authorization
            await next(context);
            return;
        }
        _logger.LogInformation("Entering authorization");
        // get token from request header
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();


        // if token is null then throw exception
        if (token is null) throw new AuthenticationException("Null or invalid token");

        // validate token
        var userId = await tokenService.ValidateToken(token);

        // if token is invalid then throw exception
        if (userId is null) throw new AuthenticationException("Invalid token");

        // get user by id
        var getUserByIdQuery = new GetUserByIdQuery(userId.Value);

        // set user in HttpContext.Items["User"]

        var user = await userQueryService.Handle(getUserByIdQuery);
        _logger.LogInformation("Successful authorization. Updating Context...");
        context.Items["User"] = user;
        _logger.LogInformation("Continuing with Middleware Pipeline");
        // call next middleware
        await next(context);
    }
}