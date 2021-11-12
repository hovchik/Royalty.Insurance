using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Royalty.Insurance.Settings;

namespace System.Common.Middleware
{
    public class SignalRAuthorizationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<SignalRAuthorizationMiddleware> _logger;


        public SignalRAuthorizationMiddleware(RequestDelegate next, ILogger<SignalRAuthorizationMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        /// <summary>
        /// Invoke Exception Middleware
        /// </summary>
        /// <param name="context">Http context</param>
        /// <returns></returns>
        // ReSharper disable once UnusedMember.Global
        public async Task Invoke(HttpContext context)
        {
            var request = context.Request;
            _logger.LogDebug("Signal R Authorization Middleware");
            try
            {


                // web sockets cannot pass headers so we must take the access token from query param and
                // add it to the header before authentication middleware runs
                if (request.Path.StartsWithSegments(MessageConstants.MessageHub, StringComparison.OrdinalIgnoreCase) &&
                    request.Query.TryGetValue(SystemConstants.AccessToken, out var accessToken))
                {
                    request.Headers.Add(SystemConstants.Authorization, $"{SystemConstants.AuthenticationType} {accessToken}");
                }
                await _next(context);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
