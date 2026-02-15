using System.Net.Http.Headers;
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
}