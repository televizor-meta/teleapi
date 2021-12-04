namespace TeleTwins.Twins.AlienAccess;

// TODO: проработать модель доступа
public interface ITwinAlienAccessService
{
    ValueTask<TwinAlienAccess> GetAccessType(TwinAlienAccessRequest request, CancellationToken cancellationToken);
    
    Task RequestAccess(TwinAlienAccessRequest request, CancellationToken cancellationToken);

    Task AllowAccess(TwinAlienAccessRequest request, CancellationToken cancellationToken);

    Task DenyAccess(TwinAlienAccessRequest request, CancellationToken cancellationToken);
}