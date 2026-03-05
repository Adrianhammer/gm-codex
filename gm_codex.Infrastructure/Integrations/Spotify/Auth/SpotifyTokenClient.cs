using System.Text.Json;
using gm_codex.Contracts.Abstractions.Integrations.Spotify.Interfaces;
using gm_codex.Contracts.Abstractions.Integrations.Spotify.Models;
using gm_codex.Infrastructure.Integrations.Spotify.Models;

namespace gm_codex.Infrastructure.Integrations.Spotify.Auth;

public class SpotifyTokenClient : ISpotifyTokenClient
{
    private readonly IHttpClientFactory _httpClientFactory;
    private const string Url = "https://accounts.spotify.com/api/token";

    public SpotifyTokenClient(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<SpotifyTokenModel> ExchangeCodeAsync(
        string code,
        string redirectUri,
        string clientId,
        string codeVerifier
    )
    {
        var httpClient = _httpClientFactory.CreateClient();

        var payload = new Dictionary<string, string>
        {
            { "client_id", clientId },
            { "grant_type", "authorization_code" },
            { "code", code },
            { "redirect_uri", redirectUri },
            { "code_verifier", codeVerifier },
        };

        var response = await httpClient.PostAsync(Url, new FormUrlEncodedContent(payload));
        if (!response.IsSuccessStatusCode)
        {
            var errorBody = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException(
                $"Spotify token exchange failed ({(int)response.StatusCode} {response.ReasonPhrase}. Body: {errorBody})"
            );
        }

        var json = await response.Content.ReadAsStringAsync();

        var spotifyTokenResponse =
            JsonSerializer.Deserialize<SpotifyResponseModels.SpotifyTokenResponse>(json);
        if (spotifyTokenResponse == null)
        {
            throw new InvalidOperationException($"Spotify token response deserialization failed.");
        }
        // Usikker på om det er egentlig vits med ekstra sjekk om AccessToken er null - veldig edge case i så fall
        if (string.IsNullOrWhiteSpace(spotifyTokenResponse.AccessToken))
            throw new ArgumentNullException(
                nameof(spotifyTokenResponse.AccessToken),
                "Expected Access Token to be provided."
            );

        return new SpotifyTokenModel()
        {
            AccessToken = spotifyTokenResponse.AccessToken,
            TokenType = spotifyTokenResponse.TokenType,
            Scope = spotifyTokenResponse.Scope,
            ExpiresIn = spotifyTokenResponse.ExpiresIn,
            RefreshToken = spotifyTokenResponse.RefreshToken,
        };
    }
}
