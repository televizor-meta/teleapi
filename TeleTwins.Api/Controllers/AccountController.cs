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
    [ValidateModel]
    [HttpPost("signin")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(SignInResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IDictionary<string, IEnumerable<string>>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(object), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> SignIn(
        [FromServices] ITvsLoginService service,
        [FromBody] SignInRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var response = await service.Login(request, cancellationToken);

            if (response == null)
                return Unauthorized();

            return Ok(response);
        }
        catch (Exception x)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse {Message = x.Message});
        }
    }


    [Authorize]
    [HttpGet("check-it")]
    public Task<IActionResult> CheckIt() => Task.FromResult(Ok("It works!") as IActionResult);
}