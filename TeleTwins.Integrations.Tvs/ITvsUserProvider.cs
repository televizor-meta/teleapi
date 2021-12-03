namespace TeleTwins.Integrations.Tvs;

public interface ITvsUserProvider
{
    Task<TvsUserResponse?> GetUser(string token, CancellationToken cancellationToken);
}
