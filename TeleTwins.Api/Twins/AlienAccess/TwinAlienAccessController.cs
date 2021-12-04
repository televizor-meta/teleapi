using Microsoft.AspNetCore.Mvc;

namespace TeleTwins.Twins.AlienAccess;

//[Authorize]
[ApiController]
[Route("api/access")]
public class TwinAlienAccessController : ControllerBase
{
    private readonly ITwinAlienAccessService _accessService;

    public TwinAlienAccessController(ITwinAlienAccessService accessService) => _accessService = accessService;


    [HttpPost]
    public async Task<IActionResult> RequestAccess(
        [FromBody] TwinAlienAccessRequest request,
        CancellationToken cancellationToken)
    {
        var callerId = HttpContext.GetUserId();

        // TODO: вынести в политику авторизации
        if (callerId != request.TargetId)
        {
            var accessType = await _accessService.GetAccessType(request, cancellationToken);
            if (accessType is TwinAlienAccess.NoAccess) return Forbid();
        }

        await _accessService.RequestAccess(request, cancellationToken);

        return Accepted();
    }

    [HttpPut]
    public async Task<IActionResult> AllowAccess(
        [FromBody] TwinAlienAccessRequest request,
        CancellationToken cancellationToken)
    {
        var callerId = HttpContext.GetUserId();

        // TODO: вынести в политику авторизации
        if (callerId != request.TargetId)
        {
            var accessType = await _accessService.GetAccessType(request, cancellationToken);
            if (accessType is TwinAlienAccess.NoAccess) return Forbid();
        }

        await _accessService.AllowAccess(request, cancellationToken);

        return Ok();
    }
    
    [HttpDelete]
    public async Task<IActionResult> DenyAccess(
        [FromBody] TwinAlienAccessRequest request,
        CancellationToken cancellationToken)
    {
        var callerId = HttpContext.GetUserId();

        // TODO: вынести в политику авторизации
        if (callerId != request.TargetId)
        {
            var accessType = await _accessService.GetAccessType(request, cancellationToken);
            if (accessType is TwinAlienAccess.NoAccess) return Forbid();
        }

        await _accessService.DenyAccess(request, cancellationToken);

        return Ok();
    }
}