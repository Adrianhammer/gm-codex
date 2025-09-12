namespace gm_codex.Application.Common;

public class Result<T>
{
    public bool Success { get; }
    public T? Value { get; }
    public string? Error  { get; }
    
    private Result(bool success, T? value, string? error)
    {
        Success = success;
        Value = value;
        Error = error;
    }

    public static Result<T> Ok(T value)
    {
        return new Result<T>(true, value, null);
    }

    public static Result<T> Fail(string? error)
    {
        return new Result<T>(false, default, error);
    }
}