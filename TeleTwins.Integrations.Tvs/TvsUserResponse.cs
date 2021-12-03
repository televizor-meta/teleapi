using System.Text.Json.Serialization;

namespace TeleTwins.Integrations.Tvs;

public class TvsUserResponse
{
    [JsonPropertyName("sub")]
    public Guid Id { get; set; }
    
    [JsonPropertyName("given_name")]
    public string FirstName { get; set; }
    
    [JsonPropertyName("family_name")]
    public string LastName { get; set; }
    
    [JsonPropertyName("username")]
    public string Login { get; set; }
}