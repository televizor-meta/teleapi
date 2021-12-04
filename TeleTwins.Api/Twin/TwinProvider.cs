namespace TeleTwins.Twin;

public class TwinProvider : ITwinProvider
{
    public Task<TeleTwins.Twin.Twin?> GetTeleTwinAsync(Guid? userId, CancellationToken cancellationToken) => Task.FromResult<TeleTwins.Twin.Twin?>(null);

}