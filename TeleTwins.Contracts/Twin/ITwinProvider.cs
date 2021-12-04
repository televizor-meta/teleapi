namespace TeleTwins.Twin;

public interface ITwinProvider
{
    Task<Twin?> GetTeleTwinAsync(Guid? userId, CancellationToken cancellationToken);
}