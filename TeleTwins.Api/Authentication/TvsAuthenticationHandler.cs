using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using TeleTwins.Integrations.Tvs;

namespace Rostelecom.DigitalProfile.Api.Authentication;

public class TvsAuthenticationHandler : AuthenticationHandler<TvsAuthenticationOptions>
{
    private readonly ITvsUserProvider _userProvider;
    
    public TvsAuthenticationHandler(
        IOptionsMonitor<TvsAuthenticationOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock, ITvsUserProvider userProvider)
        : base(options, logger, encoder, clock)
        => _userProvider = userProvider;


    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        try
        {
            var header = Context.Request.Headers[Options.HeaderName];
            if (header.Count is 0)
                return AuthenticateResult.NoResult();

            var token = header.ToString();
            
            if (string.IsNullOrWhiteSpace(token))
                return AuthenticateResult.NoResult();

            var user = await _userProvider.GetUser(token, CancellationToken.None);
            
            if (user is null)
                return AuthenticateResult.NoResult();

            var principal = new ClaimsPrincipal();
            var identity = new ClaimsIdentity(Scheme.Name);
            
            identity.AddClaim(new Claim(ClaimTypes.Sid, user.Id.ToString()));
            principal.AddIdentity(identity);

            var ticket = new AuthenticationTicket(principal, Scheme.Name);
            
            return AuthenticateResult.Success(ticket);
        }
        catch (Exception x)
        {
            return AuthenticateResult.Fail(x.Message);
        }
    }
}