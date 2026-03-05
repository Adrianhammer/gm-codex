using gm_codex.Contracts.Abstractions.Integrations.Spotify.Models;

namespace gm_codex.Contracts.Abstractions.Integrations.Spotify.Interfaces;

public interface ISpotifyPlaybackClient
{
    Task<SpotifyUserProfile> GetProfileAsync(string accessToken);
    Task StartPlaybackAsync(string accessToken, string contextUri);
}
