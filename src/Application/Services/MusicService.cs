using gm_codex.Application.Common;

namespace gm_codex.Application.Services;

public class MusicService 
{
    public MusicService()
    {
        
    }

    public async Task<Result<int>> PlayMusicAsync()
    {
        try
        {
            int a = 1;
            
            return a > 0 ? Result<int>.Ok(a) : Result<int>.Fail("Something went wrong");
        }
        catch (Exception e)
        {
            return Result<int>.Fail(e.Message);
        }
    }
}