namespace TeleTwins.Twins.Publications;

public interface ITwinPublicationProvider
{
    Task<IReadOnlyCollection<TwinPublication>> GetPublicationListAsync(
        TwinPublicationQuery query,
        CancellationToken cancellationToken);

    Task<TwinPublication?> GetPublicationAsync(Guid id, CancellationToken cancellationToken);
}