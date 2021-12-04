using TeleTwins.Twins;

namespace TeleTwins.Twin;

public class TwinProvider : ITwinProvider
{
    public Task<Twins.Twin?> GetTwinAsync(Guid? userId, CancellationToken cancellationToken) => Task.FromResult<Twins.Twin?>(null);

}