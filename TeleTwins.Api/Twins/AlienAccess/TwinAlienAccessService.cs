namespace TeleTwins.Twins.AlienAccess;

public class TwinAlienAccessService : ITwinAlienAccessService
{
    public ValueTask<TwinAlienAccess> GetAccessType(TwinAlienAccessRequest request, CancellationToken cancellationToken)
        => ValueTask.FromResult(TwinAlienAccess.Permanent);

    public Task RequestAccess(TwinAlienAccessRequest request, CancellationToken cancellationToken) => Task.CompletedTask;

    public Task AllowAccess(TwinAlienAccessRequest request, CancellationToken cancellationToken) => Task.CompletedTask;

    public Task DenyAccess(TwinAlienAccessRequest request, CancellationToken cancellationToken) => Task.CompletedTask;
}