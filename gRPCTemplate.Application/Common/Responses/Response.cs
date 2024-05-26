using gRPCTemplate.Domain.Common;

namespace gRPCTemplate.Application.Common.Responses;

public class Response<T> where T : class, new()
{
    public T Success { get; set; }
    public ErrorMessage Error { get; set; }
}
