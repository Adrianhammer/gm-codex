using gm_codex.Contracts.Abstractions.Integrations.Spotify.Models;

namespace gm_codex.Contracts.Abstractions.Integrations.Spotify.Interfaces;

public interface ISpotifyTokenClient
{
    Task<SpotifyTokenModel> ExchangeCodeAsync(
        string code,
        string redirectUri,
        string clientId,
        string codeVerifier
    );
}
