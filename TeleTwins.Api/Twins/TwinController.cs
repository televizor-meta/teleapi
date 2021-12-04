using Microsoft.AspNetCore.Mvc;
using TeleTwins.DataWarehouse;
using TeleTwins.Twins.AlienAccess;

namespace TeleTwins.Twins;

//[Authorize]
[ApiController]
[Route("api/[controller]/{id:guid}")]
public class TwinController : ControllerBase
{
    private readonly ITwinAlienAccessService _accessService;

    public TwinController(ITwinAlienAccessService accessService) => _accessService = accessService;

    
    [HttpGet]
    [Produces(typeof(Twin))]
    public async Task<IActionResult> GetProfile(
        [FromServices] ITwinProvider provider,
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var callerId = HttpContext.GetUserId();
        
        if (id != callerId)
        {
            var command = new TwinAlienAccessRequest { AlienId = callerId!.Value, TargetId = id };
            var accessType = await _accessService.GetAccessType(command, cancellationToken);
            if (accessType is TwinAlienAccess.NoAccess) return Forbid();
            // TODO: add OneTime handling
        }
        
        var twin = await provider.GetTwinAsync(id, cancellationToken);
        return twin is null ? NotFound() : Ok(twin);
    }

    [HttpPost]
    [Consumes("application/json")]
    public async Task<IActionResult> StartMining(
        [FromServices] IDataWarehouse warehouse,
        [FromRoute] Guid id,
        [FromBody] MiningParameters parameters,
        CancellationToken cancellationToken)
    {
        if (id != HttpContext.GetUserId())
            return Forbid();
        
        await warehouse.StartMiningFor(id, cancellationToken);
        return Accepted();
    }
}