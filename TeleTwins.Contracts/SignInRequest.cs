using System.Text.Json.Serialization;

namespace TeleTwins.Contracts;

public class SignInRequest
{
    [JsonPropertyName("login")]
    public string Login { get; init; }
    
    [JsonPropertyName("password")]
    public string Password { get; init; }
}