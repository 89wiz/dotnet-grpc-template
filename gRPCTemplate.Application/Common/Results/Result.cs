using gRPCTemplate.Domain.Common;

namespace gRPCTemplate.Application.Common.Results;

public class Result<T> where T : class, new()
{
    public T? Success { get; set; }
    public ErrorMessage? Error { get; set; }

    public static Result<T> Create(T success)
    {
        return new Result<T>
        {
            Success = success
        };
    }

    public static Result<T> Create(ErrorMessage error)
    {
        return new Result<T>
        {
            Error = error
        };
    }
}
