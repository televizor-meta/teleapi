using System.Text.Json.Serialization;

namespace TeleTwins.Contracts;

public class SignInResponse
{
    [JsonPropertyName("access_token")]
    public string Token { get; set; }
}