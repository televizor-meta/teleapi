using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rostelecom.DigitalProfile.Api.Authentication;
using TeleTwins.Contracts;
using TeleTwins.Integrations.Tvs;

namespace Rostelecom.DigitalProfile.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    private static Guid _id = Guid.NewGuid();
    
    // [ValidateModel]
    // [HttpPost("signin")]
    // [Consumes("application/json")]
    // [Produces("application/json")]
    // [ProducesResponseType(typeof(SignInResponse), StatusCodes.Status200OK)]
    // [ProducesResponseType(typeof(IDictionary<string, IEnumerable<string>>), StatusCodes.Status400BadRequest)]
    // [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    // [ProducesResponseType(typeof(object), StatusCodes.Status401Unauthorized)]
    // public async Task<IActionResult> SignIn(
    //     [FromServices] ITvsLoginService service,
    //     [FromBody] SignInRequest request,
    //     CancellationToken cancellationToken)
    // {
    //     try
    //     {
    //         var response = await service.Login(request, cancellationToken);
    //
    //         if (response == null)
    //             return Unauthorized();
    //
    //         return Ok(response);
    //     }
    //     catch (Exception x)
    //     {
    //         return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse {Message = x.Message});
    //     }
    // }

    [HttpGet("login")]
    public IActionResult Login() =>
        Redirect("https://tvscp.tionix.ru/realms/master/protocol/openid-connect/auth/" +
                 "?response_type=code&client_id=tvscp&scope=openid"
                 $"&redirect_uri={Url.Action(nameof(SignInComplete), new { sid = _id })} ");
    
    [HttpGet("signinсomplete")]
    public Task<IActionResult> SignInComplete(string sid)
    {
        return Task.FromResult(Ok() as IActionResult);
    }

    [HttpGet]
    public Task<IActionResult> GetAccessToken(string sid)
    {
        return null;
    }
    
    [Authorize]
    [HttpGet("check-it")]
    public Task<IActionResult> CheckIt() => Task.FromResult(Ok("It works!") as IActionResult);
}