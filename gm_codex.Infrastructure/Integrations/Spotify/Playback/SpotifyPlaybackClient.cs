using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using gm_codex.Infrastructure.Integrations.Spotify.Models;

namespace gm_codex.Infrastructure.Integrations.Spotify.Playback;

public class SpotifyPlaybackClient
{
    private readonly IHttpClientFactory _httpClientFactory;
    
    public SpotifyPlaybackClient(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<SpotifyResponseModels.SpotifyUserProfileResponse> GetProfileAsync(string accessToken)
    {
            var httpClient  = _httpClientFactory.CreateClient();
        
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        
            var response = await httpClient.GetAsync("https://api.spotify.com/v1/me");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
        
            var spotifyUserProfileResponse = JsonSerializer.Deserialize<SpotifyResponseModels.SpotifyUserProfileResponse>(content);

            return spotifyUserProfileResponse ?? throw new Exception("Something went wrong");
    }

    public async Task StartPlaybackAsync(string accessToken, string contextUri)
    {
        var httpClient = _httpClientFactory.CreateClient();
        
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        var payload = new Dictionary<string, string>
        {
            ["context_uri"] = contextUri
        };
        
        var response = await httpClient.PutAsJsonAsync("https://api.spotify.com/v1/me/player/play", payload);
        
        response.EnsureSuccessStatusCode();
    }
    
}