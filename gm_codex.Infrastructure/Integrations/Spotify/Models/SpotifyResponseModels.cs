using System.Text.Json.Serialization;

namespace gm_codex.Infrastructure.Integrations.Spotify.Models;

public class SpotifyResponseModels
{
    public class SpotifyTokenResponse
    {
        [JsonPropertyName("access_token")] public string? AccessToken { get; set; }
        [JsonPropertyName("token_type")] public string? TokenType { get; set; }
        [JsonPropertyName("scope")] public string? Scope { get; set; }
        [JsonPropertyName("expires_in")] public int? ExpiresIn { get; set; }
        [JsonPropertyName("refresh_token")] public string? RefreshToken { get; set; }
    }
    
    public class SpotifyUserProfileResponse
    {
        [JsonPropertyName("display_name")] public string? DisplayName { get; set; }
        [JsonPropertyName("id")] public string? Id { get; set; }
        [JsonPropertyName("type")] public string? Type { get; set; }
        [JsonPropertyName("country")] public string? Country { get; set; }
        [JsonPropertyName("uri")] public string? Uri { get; set; }
    }
}