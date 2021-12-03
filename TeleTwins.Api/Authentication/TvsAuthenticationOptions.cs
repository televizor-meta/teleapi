using Microsoft.AspNetCore.Authentication;

namespace Rostelecom.DigitalProfile.Api.Authentication;

public class TvsAuthenticationOptions : AuthenticationSchemeOptions
{
    public string HeaderName { get; set; } = "X-TeleTwins-Access-Token";
}