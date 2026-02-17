using Xunit;

namespace gm_codex.Tests;

public class UnitTest1
{
    [Fact]
    public void Addin_two_and_two_equals_four()
    {
        int a = 2, b = 2;
        
        var result = a + b;
        
        Assert.Equal(4, result);
    }
}
