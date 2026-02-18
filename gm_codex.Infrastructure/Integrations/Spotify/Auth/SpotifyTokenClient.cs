using System.Text.Json;
using gm_codex.Infrastructure.Integrations.Spotify.Models;

namespace gm_codex.Infrastructure.Integrations.Spotify.Auth;

public class SpotifyTokenClient
{

    private readonly IHttpClientFactory _httpClientFactory;
    
    public SpotifyTokenClient(IHttpClientFactory httpClientFactory)
    {
            _httpClientFactory = httpClientFactory;
    }

    public async Task<SpotifyResponseModels.SpotifyTokenResponse> ExchangeCodeAsync(string code, string redirectUri, string clientId,
        string codeVerifier)
    {
        const string url = "https://accounts.spotify.com/api/token";
        
        Dictionary<string, string> payload = new Dictionary<string, string>
        {
            { "client_id", clientId },
            { "grant_type", "authorization_code" },
            { "code", code },
            { "redirect_uri", redirectUri },
            { "code_verifier", codeVerifier }
        };
        
        var httpClient = _httpClientFactory.CreateClient();
        
        var response = await httpClient.PostAsync(url, new FormUrlEncodedContent(payload));
        if (!response.IsSuccessStatusCode)
        {
            var errorBody  = await response.Content.ReadAsStringAsync();
            throw new Exception($"Spotify token exchange failed ({(int)response.StatusCode} {response.ReasonPhrase}. Body: {errorBody}) )");
        }
        
        var json = await response.Content.ReadAsStringAsync();
        
        SpotifyResponseModels.SpotifyTokenResponse? spotifyTokenResponse = JsonSerializer.Deserialize<SpotifyResponseModels.SpotifyTokenResponse>(json);

        return spotifyTokenResponse ?? throw new InvalidOperationException(
            $"Spotify token response could not be deserialized. Response body did not match expected schema. Body: {json}");
    }
}