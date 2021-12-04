using Microsoft.AspNetCore.Mvc;
using TeleTwins.Api;

namespace TeleTwins.Twin;

[ApiController]
[Route("api/[controller]")]
public class TwinController : ControllerBase
{
    private readonly ITwinProvider _teleTwinProvider;
    
    [HttpGet("{id:guid?}")]
    [Produces(typeof(Twin))]
    [Consumes("application/json")]
    //[Authorize]
    public async Task<IActionResult> GetProfile(Guid? id, CancellationToken cancellationToken)
    {
        id ??= HttpContext.GetUserId();
        
        /*
         * TODO: Добавить проверку прав доступа
         * if (userId != GetUserId())
         * {
         *      1. Полчить права доступа
         *      2. Если 
         * }
         *
         * ...
         * return Forbid();
         */

        var twin = await _teleTwinProvider.GetTeleTwinAsync(id, cancellationToken);

        return twin is null ? NotFound() : Ok(twin);
    }
}