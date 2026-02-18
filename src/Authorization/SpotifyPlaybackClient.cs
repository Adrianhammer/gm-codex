using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using gm_codex.Application.Common;

namespace gm_codex.Authorization;

public class SpotifyPlaybackClient
{
    private readonly IHttpClientFactory _httpClientFactory;
    
    public SpotifyPlaybackClient(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<Result<SpotifyResponseModels.SpotifyUserProfileResponse>> GetProfileAsync(string accessToken)
    {
        var httpClient  = _httpClientFactory.CreateClient();
        
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        
        var response = await httpClient.GetAsync("https://api.spotify.com/v1/me");
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        
        var spotifyUserProfileResponse = JsonSerializer.Deserialize<SpotifyResponseModels.SpotifyUserProfileResponse>(content);
        
        return spotifyUserProfileResponse?.DisplayName != null
            ? Result<SpotifyResponseModels.SpotifyUserProfileResponse>.Ok(spotifyUserProfileResponse)
            : Result<SpotifyResponseModels.SpotifyUserProfileResponse>.Fail("Something went wrong: " + content);
    }

    public async Task<Result<string>> StartPlaybackAsync(string accessToken)
    {
        var httpClient = _httpClientFactory.CreateClient();
        
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        var payload = new Dictionary<string, string>
        {
            ["context_uri"] = "spotify:album:5ht7ItJgpBH7W6vJ5BqpPr"
        };
        
        var response = await httpClient.PutAsJsonAsync("https://api.spotify.com/v1/me/player/play");
        
        return response.EnsureSuccessStatusCode().IsSuccessStatusCode
            ? Result<string>.Ok("Playback started")
            : Result<string>.Fail("Something went wrong: " + response.Content.ReadAsStringAsync().Result);
    }
    
}