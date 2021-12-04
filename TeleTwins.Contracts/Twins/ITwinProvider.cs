namespace TeleTwins.Twins;

public interface ITwinProvider
{
    Task<Twins.Twin?> GetTwinAsync(Guid? userId, CancellationToken cancellationToken);
}