using System.Text.Json;
using gm_codex.Application.Common;

namespace gm_codex.Authorization;

public class SpotifyTokenClient
{

    private readonly IHttpClientFactory _httpClientFactory;
    
    public SpotifyTokenClient(IHttpClientFactory httpClientFactory)
    {
            _httpClientFactory = httpClientFactory;
    }

    public async Task<Result<string?>> ExchangeCodeAsync(string code, string redirectUri, string clientId,
        string codeVerifier)
    {
        
        var url = "https://accounts.spotify.com/api/token";
        
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
            return Result<string?>.Fail("something went wrong: " + await response.Content.ReadAsStringAsync());
        }
        
        var json = await response.Content.ReadAsStringAsync();
        
        SpotifyResponseModels.SpotifyTokenResponse? spotifyTokenResponse = JsonSerializer.Deserialize<SpotifyResponseModels.SpotifyTokenResponse>(json);
        
        return spotifyTokenResponse is not null 
            ? Result<string?>.Ok(spotifyTokenResponse.AccessToken) 
            : Result<string?>.Fail("something went wrong");
    }
}