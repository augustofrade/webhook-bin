namespace WebhookBin.Domain.Common;
public class Result
{
    protected Result(Error? error)
    {
        Error = error;
    }

    public Error? Error { get; }

    public bool IsSuccess() => Error is null;
    public bool IsFailure() => Error is not null;

    public static Result Success() => new Result(null);

    public static Result Failure(Error error)
    {
        ArgumentNullException.ThrowIfNull(error);
        return new Result(error);
    }

    public static implicit operator Result(Error error) => Failure(error);
}

public class Result<T> : Result
{
    private readonly T? _value;

    private Result(T value) : base(null)
    {
        _value = value;
    }

    private Result(Error error) : base(error)
    {
        _value = default;
    }

    public T Value =>
        IsFailure() || _value is null
            ? throw new InvalidOperationException("Cannot access a value of a failed Result.")
            : _value;

    public static Result<T> Success(T value) => new Result<T>(value);

    public static new Result<T> Failure(Error error)
    {
        ArgumentNullException.ThrowIfNull(error);
        return new Result<T>(error);
    }

    public static implicit operator Result<T>(Error error) => Failure(error);
    public static implicit operator Result<T>(T data) => Success(data);
}