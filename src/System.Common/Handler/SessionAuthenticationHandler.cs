
using System.Collections.Generic;
using System.Common.Extensions;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Royalty.Insurance.Settings;

namespace System.Common.Handler
{
    //TODO: research and remove
    //public class SessionAuthenticationHandler : AuthenticationHandler<SessionHashAuthenticationSchemeOptions>
    //{
    //    private readonly IMemoryCache _memoryCache;
    //    private readonly ICurrentUserService 

    //    public SessionAuthenticationHandler(IOptionsMonitor<SessionHashAuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock, IMemoryCache memoryCache) : base(options, logger, encoder, clock)
    //    {
    //        _memoryCache = memoryCache;
    //    }

    //    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    //    {
    //        var endpoint = Context.GetEndpoint();
    //        if (endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() != null)
    //        {
    //            return await Task.FromResult(AuthenticateResult.NoResult());
    //        }
    //        if (!Context.User.Identity.IsAuthenticated || !_memoryCache.TryGetValue(Context.User.UserId(), out List<Guid> sessions))
    //        {
    //            return await Task.FromResult(AuthenticateResult.Fail(SystemConstants.Unauthorized));
    //        }
    //        if (!Request.Headers.ContainsKey(SystemConstants.Authorization))
    //            return await Task.FromResult(AuthenticateResult.Fail(ResourceCommonMessage.MissingAuthorizationHeader));

    //        Claim sessionClaim = Context.User.Claims.FirstOrDefault(item => item.Type.Equals(SystemConstants.SessionId));

    //        if (sessionClaim != null && !sessions.Contains(Guid.Parse(sessionClaim.Value)))
    //        {
    //            return await Task.FromResult(AuthenticateResult.Fail(SystemConstants.Unauthorized));
    //        }

    //        var ticket = new AuthenticationTicket(Context.User, Scheme.Name);
    //        return await Task.FromResult(AuthenticateResult.Success(ticket));
    //    }
    }
//}