using System.Security.Cryptography;
using System.Text;
using gm_codex.Authorization;
using Xunit;

namespace gm_codex.Tests.Authorization;

public class PkceTests
{
    [Fact]
    public void Pkce_Should_Generate_CodeChallenge_And_Verifier()
    {
        var (challenge, verifier) = Pkce.Generate(32);

        Assert.False(string.IsNullOrWhiteSpace(verifier));
        Assert.False(string.IsNullOrWhiteSpace(challenge));
        Assert.Matches("^[A-Za-z0-9_-]+$", verifier);
        Assert.Matches("^[A-Za-z0-9_-]+$", challenge);

        var hash = SHA256.HashData(Encoding.UTF8.GetBytes(verifier));
        var expectedChallenge = Base64UrlEncode(hash);
        Assert.Equal(expectedChallenge, challenge);
    }

    [Fact]
    public void Pkce_Should_Respect_Requested_Size()
    {
        var (challenge, verifier) = Pkce.Generate(64);

        Assert.Equal(86, verifier.Length);
        Assert.False(string.IsNullOrWhiteSpace(challenge));
    }

    private static string Base64UrlEncode(byte[] data) =>
        Convert.ToBase64String(data)
            .Replace('+', '-')
            .Replace('/', '_')
            .TrimEnd('=');
}
