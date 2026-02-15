using System.Diagnostics;
using Microsoft.Extensions.Configuration;

namespace gm_codex.Authorization;

public class RequestUserAuth
{
    private string? _clientID;
    private string? _redirectUri;

    public RequestUserAuth(IConfiguration configuration)
    {
        _clientID = configuration["Spotify:ClientID"];
        _redirectUri = configuration["Spotify:RedirectUri"];
    }

    public static string BuildAuthUrl(string clientId, string redirectUri, string codeChallenge)
    {
        var query = new Dictionary<string, string>
        {
            ["response_type"] = "code",
            ["client_id"] = clientId,
            ["scope"] = "user-modify-playback-state",
            ["code_challenge_method"] = "S256",
            ["code_challenge"] = codeChallenge,
            ["redirect_uri"] = redirectUri,
        };

        var qs = string.Join("&",
            query.Select(kv => $"{Uri.EscapeDataString(kv.Key)}={Uri.EscapeDataString(kv.Value)}"));
        
        return $"https://accounts.spotify.com/authorize?{qs}";
    }

    public static void OpenAuthorizeUrl(string url)
    {
        // Responsibility: open browser
        Process.Start(new ProcessStartInfo
        {
            FileName = url,
            UseShellExecute = true
        });
    }

}