using Microsoft.AspNetCore.Mvc;
using TeleTwins.Twins.AlienAccess;

namespace TeleTwins.Twins.Publications;

//[Authorize]
[ApiController]
[Route("api/twin/{twinId:guid}/publication")]
public class TwinPublicationController : ControllerBase
{
    private readonly ITwinAlienAccessService _accessService;

    public TwinPublicationController(ITwinAlienAccessService accessService) => _accessService = accessService;

    
    [HttpGet]
    [Produces(typeof(IReadOnlyCollection<TwinPublication>))]
    public async Task<IActionResult> GetList(
        [FromServices] ITwinPublicationProvider provider,
        [FromQuery] Guid twinId,
        [FromQuery] TwinPublicationQuery query,
        CancellationToken cancellationToken)
    {
        var callerId = HttpContext.GetUserId();

        // TODO: вынести в политику авторизации
        if (callerId != twinId)
        {
            var command = new TwinAlienAccessRequest { AlienId = callerId!.Value, TargetId = twinId };
            var accessType = await _accessService.GetAccessType(command, cancellationToken);
            if (accessType is TwinAlienAccess.NoAccess) return Forbid();
        }
        
        var result = await provider.GetPublicationListAsync(query, cancellationToken);
        return result.Count is 0 ? NoContent() : Ok(result);
    }

    [HttpGet("{publicationId:guid}")]
    [Produces(typeof(TwinPublication))]
    public async Task<IActionResult> GetSingle(
        [FromServices] ITwinPublicationProvider provider,
        [FromRoute] Guid twinId,
        [FromRoute] Guid publicationId,
        CancellationToken cancellationToken)
    {
        var callerId = HttpContext.GetUserId();

        // TODO: вынести в политику авторизации
        if (callerId != twinId)
        {
            var command = new TwinAlienAccessRequest { AlienId = callerId!.Value, TargetId = twinId };
            var accessType = await _accessService.GetAccessType(command, cancellationToken);
            if (accessType is TwinAlienAccess.NoAccess) return Forbid();
        }
        
        var publication = await provider.GetPublicationAsync(publicationId, cancellationToken);
        return publication is null || publication.TwinId != twinId
             ? NotFound()
             : Ok(publication);
    }
}