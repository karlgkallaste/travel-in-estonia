namespace TravelInEstonia.Domain.Infrastructure;

public sealed class Result
{
    public string? Error { get; }

    public bool IsSuccess { get; }


    private Result()
    {
        IsSuccess = true;
        Error = null;
    }

    private Result(string error)
    {
        Error = error ?? throw new ArgumentNullException(nameof(error), "Error cannot be null.");
        IsSuccess = false;
        Error = error;
    }


    public static Result Success() => new Result();

    public static Result Failure(string error) => new(error);
}