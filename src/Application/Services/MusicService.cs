using System.Net;
using gm_codex.Application.Common;
using gm_codex.Authorization;

namespace gm_codex.Application.Services;

public class MusicService 
{
    private readonly SpotifyTokenClient _spotifyTokenClient;
    private readonly SpotifyPlaybackClient _spotifyPlaybackClient;
    public MusicService(SpotifyTokenClient spotifyTokenClient, SpotifyPlaybackClient spotifyPlaybackClient)
    {
        _spotifyTokenClient =  spotifyTokenClient;
        _spotifyPlaybackClient = spotifyPlaybackClient;
    }

    public async Task<Result<SpotifyResponseModels.SpotifyUserProfileResponse>> GetProfileAsync()
    {
        try
        {
            var pkce= Pkce.Generate();

            var authUrl = RequestUserAuth.BuildAuthUrl("10d87a3d47dc45c19d2cd21e343a3d0a",
                "http://127.0.0.1:8000/callback/", pkce.code_challenge);
            
            using var listener = new HttpListener();
            listener.Prefixes.Add("http://127.0.0.1:8000/callback/");
            Console.WriteLine("Starting listener...");
            listener.Start();
            Console.WriteLine("Listener started.");
            Console.WriteLine("Opening browser...");
            
            RequestUserAuth.OpenAuthorizeUrl(authUrl);
            Console.WriteLine("Waiting for callback...");
            
            var ctx = await listener.GetContextAsync();
            Console.WriteLine("Callback received.");

            var code = ctx.Request.QueryString["code"];
            var error = ctx.Request.QueryString["error"];

            ctx.Response.StatusCode = 200;
            ctx.Response.ContentType = "text/plain; charset=utf-8";
            
            using var writer = new StreamWriter(ctx.Response.OutputStream);
            await writer.WriteAsync("Authorization complete. You can close this tab.");
            await writer.FlushAsync();
            ctx.Response.Close();
            
            if (!string.IsNullOrWhiteSpace(error) || string.IsNullOrWhiteSpace(code))
            {
                return Result<SpotifyResponseModels.SpotifyUserProfileResponse>.Fail("Something went wrong: "  + error);
            }
                        
            var tokenResult = await _spotifyTokenClient.ExchangeCodeAsync(
                code,
                "http://127.0.0.1:8000/callback/",
                "10d87a3d47dc45c19d2cd21e343a3d0a",
                pkce.verifier);

            if (tokenResult.Error != null || tokenResult.Value == null)
            {
                return Result<SpotifyResponseModels.SpotifyUserProfileResponse>.Fail(tokenResult.Error);
            }
            
            var output = await _spotifyPlaybackClient.GetProfileAsync(tokenResult.Value);

            if (output.Error != null || output.Value == null)
            {
                return Result<SpotifyResponseModels.SpotifyUserProfileResponse>.Fail(output.Error);
            }
            
            return output.Success ? Result<SpotifyResponseModels.SpotifyUserProfileResponse>.Ok(output.Value) : Result<SpotifyResponseModels.SpotifyUserProfileResponse>.Fail("Token exchange failed: " + output.Error);
            
        }
        catch (Exception e)
        {
            return Result<SpotifyResponseModels.SpotifyUserProfileResponse>.Fail(e.Message);
        }
    }
}