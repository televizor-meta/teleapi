using TeleTwins.Contracts;

namespace TeleTwins.Integrations.Tvs;

public interface ITvsLoginService
{
    Task<SignInResponse?> Login(SignInRequest request, CancellationToken cancellationToken);
}
