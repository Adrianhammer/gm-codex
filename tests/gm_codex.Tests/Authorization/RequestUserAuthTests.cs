using System.Web;
using gm_codex.Authorization;
using Xunit;

namespace gm_codex.Tests.Authorization;

public class RequestUserAuthTests
{
    [Fact]
    public void BuildAuthUrl_Should_Construct_Authorize_Url()
    {
        var clientId = "client_123";
        var redirectUri = "http://127.0.0.1:8080/callback";
        var codeChallenge = "abcDEF123_-";
        
        
        var url = RequestUserAuth.BuildAuthUrl(clientId, redirectUri, codeChallenge);
        var uri = new Uri(url);

        var expectedBaseUrl = "https://accounts.spotify.com/authorize";
        Assert.Equal(expectedBaseUrl, uri.GetLeftPart(UriPartial.Path));
        
        
        var query = HttpUtility.ParseQueryString(uri.Query);
        
        Assert.Equal("code", query["response_type"]);
        Assert.Equal(clientId, query["client_id"]);
        Assert.Equal("user-modify-playback-state", query["scope"]);
        Assert.Equal("S256", query["code_challenge_method"]);
        Assert.Equal(codeChallenge, query["code_challenge"]);
        Assert.Equal(redirectUri, query["redirect_uri"]);
        

    }

    [Fact]
    public void BuildAuthUrl_Should_UrlEncode_Query_Values()
    {

        var clientId = "client_123";
        var redirectUri = "http://127.0.0.1:8080/callback?x=1&y=2";
        var codeChallenge = "abcDEF123_-";
        
        var url = RequestUserAuth.BuildAuthUrl(clientId, redirectUri, codeChallenge);
        
        Assert.Contains("%3A%2F", url);
        
        var uri = new Uri(url);
        var query = HttpUtility.ParseQueryString(uri.Query);
        Assert.Equal(redirectUri, query["redirect_uri"]);
    }
}
