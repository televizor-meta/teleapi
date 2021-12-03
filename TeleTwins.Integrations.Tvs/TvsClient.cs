using System.Net.Http.Json;
using System.Text.Json;
using TeleTwins.Contracts;

namespace TeleTwins.Integrations.Tvs;

public class TvsClient : ITvsLoginService, ITvsUserProvider
{
    private const string LoginPath = "/realms/master/protocol/openid-connect/token";
    private const string UserInfoPath = "/realms/master/protocol/openid-connect/userinfo";
    
    private readonly TvsClientOptions _options;


    public TvsClient(TvsClientOptions options) =>
        _options = options ?? throw new ArgumentNullException(nameof(options));

    public async Task<SignInResponse?> Login(SignInRequest request, CancellationToken cancellationToken)
    {
        using var client = new HttpClient();

        var uri = new Uri(new Uri(_options.BaseUrl), LoginPath);

        var formContent = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("client_id", _options.ServiceLogin), 
            new KeyValuePair<string, string>("client_secret", _options.ServiceSecret),
            new KeyValuePair<string, string>("grant_type", "password"),
            new KeyValuePair<string, string>("username", request.Login),
            new KeyValuePair<string, string>("password", request.Password),
            new KeyValuePair<string, string>("scope", "openid")
        });

        var response = await client.PostAsync(uri, formContent, cancellationToken);

        if (!response.IsSuccessStatusCode)
            throw new InvalidOperationException(
                await response.Content.ReadAsStringAsync(cancellationToken));

        var responseBody = await response.Content.ReadAsStreamAsync(cancellationToken); 
        return await JsonSerializer.DeserializeAsync<SignInResponse>(
            responseBody, cancellationToken: cancellationToken);
    }

    public async Task<TvsUserResponse?> GetUser(string token, CancellationToken cancellationToken)
    {
        var uri = new Uri(new Uri(_options.BaseUrl), UserInfoPath);
        
        using var client = new HttpClient();
        using var request = new HttpRequestMessage(HttpMethod.Get, uri);
        
        request.Headers.Add("'Authorization", $"Bearer {token}");

        var response = await client.SendAsync(request, cancellationToken);
        
        return await JsonSerializer.DeserializeAsync<TvsUserResponse>(
            await response.Content.ReadAsStreamAsync(cancellationToken),
            cancellationToken: cancellationToken);
    }
}