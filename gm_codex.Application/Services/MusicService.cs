using System.Net;
using gm_codex.Application.Common;
using gm_codex.Application.Security;
using gm_codex.Contracts.Abstractions.Integrations.Spotify.Interfaces;
using gm_codex.Contracts.Abstractions.Integrations.Spotify.Models;
using gm_codex.Infrastructure.Integrations.Spotify.Auth;

namespace gm_codex.Application.Services;

public class MusicService
{
    private readonly ISpotifyTokenClient _spotifyTokenClient;
    private readonly ISpotifyPlaybackClient _spotifyPlaybackClient;

    public MusicService(
        ISpotifyTokenClient spotifyTokenClient,
        ISpotifyPlaybackClient spotifyPlaybackClient
    )
    {
        _spotifyTokenClient = spotifyTokenClient;
        _spotifyPlaybackClient = spotifyPlaybackClient;
    }

    public async Task<Result<SpotifyUserProfile>> GetProfileAsync()
    {
        try
        {
            var pkce = Pkce.Generate();

            var authUrl = RequestUserAuth.BuildAuthUrl(
                "10d87a3d47dc45c19d2cd21e343a3d0a",
                "http://127.0.0.1:8000/callback/",
                pkce.code_challenge
            );

            using var listener = new HttpListener();
            listener.Prefixes.Add("http://127.0.0.1:8000/callback/");
            listener.Start();

            RequestUserAuth.OpenAuthorizeUrl(authUrl);

            var ctx = await listener.GetContextAsync();

            var code = ctx.Request.QueryString["code"];
            var error = ctx.Request.QueryString["error"];

            ctx.Response.StatusCode = 200;
            ctx.Response.ContentType = "text/plain; charset=utf-8";

            await using var writer = new StreamWriter(ctx.Response.OutputStream);
            await writer.WriteAsync("Authorization complete. You can close this tab.");
            await writer.FlushAsync();

            ctx.Response.Close();

            if (!string.IsNullOrWhiteSpace(error) || string.IsNullOrWhiteSpace(code))
            {
                return Result<SpotifyUserProfile>.Fail("Something went wrong: " + error);
            }

            var tokenResult = await _spotifyTokenClient.ExchangeCodeAsync(
                code,
                "http://127.0.0.1:8000/callback/",
                "10d87a3d47dc45c19d2cd21e343a3d0a",
                pkce.verifier
            );

            if (tokenResult.AccessToken is null)
            {
                return Result<SpotifyUserProfile>.Fail("Access token is null.");
            }

            var output = await _spotifyPlaybackClient.GetProfileAsync(tokenResult.AccessToken);

            if (output.DisplayName is null)
            {
                return Result<SpotifyUserProfile>.Fail("No user found.");
            }

            // Temporary. Will replace with variable
            const string album = "spotify:album:5ht7ItJgpBH7W6vJ5BqpPr";
            try
            {
                await _spotifyPlaybackClient.StartPlaybackAsync(tokenResult.AccessToken, album);
            }
            catch (Exception e)
            {
                return Result<SpotifyUserProfile>.Fail(e.Message);
            }

            return Result<SpotifyUserProfile>.Ok(output);
        }
        catch (Exception e)
        {
            return Result<SpotifyUserProfile>.Fail(e.Message);
        }
    }
}
