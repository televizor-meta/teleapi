namespace TeleTwins.Twins.Publications;

public class TwinPublicationProvider : ITwinPublicationProvider
{
    public Task<IReadOnlyCollection<TwinPublication>> GetPublicationListAsync(
        TwinPublicationQuery query,
        CancellationToken cancellationToken)
        => Task.FromResult<IReadOnlyCollection<TwinPublication>>(Array.Empty<TwinPublication>());

    public Task<TwinPublication?> GetPublicationAsync(Guid id, CancellationToken cancellationToken)
        => Task.FromResult<TwinPublication>(null);
}