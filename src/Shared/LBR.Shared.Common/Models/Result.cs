namespace LBR.Shared.Common.Models;

/// <summary>
/// Generic result pattern for operation outcomes
/// </summary>
/// <typeparam name="T">Type of data returned on success</typeparam>
public class Result<T>
{
    public bool IsSuccess { get; }
    public T? Data { get; }
    public string? Error { get; }
    public List<string>? Errors { get; }

    private Result(bool isSuccess, T? data, string? error, List<string>? errors)
    {
        IsSuccess = isSuccess;
        Data = data;
        Error = error;
        Errors = errors;
    }

    public static Result<T> Success(T data) => new(true, data, null, null);
    
    public static Result<T> Failure(string error) => new(false, default, error, null);
    
    public static Result<T> Failure(List<string> errors) => new(false, default, null, errors);
}

/// <summary>
/// Result pattern without data
/// </summary>
public class Result
{
    public bool IsSuccess { get; }
    public string? Error { get; }
    public List<string>? Errors { get; }

    private Result(bool isSuccess, string? error, List<string>? errors)
    {
        IsSuccess = isSuccess;
        Error = error;
        Errors = errors;
    }

    public static Result Success() => new(true, null, null);
    
    public static Result Failure(string error) => new(false, error, null);
    
    public static Result Failure(List<string> errors) => new(false, null, errors);
}
