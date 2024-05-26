using MediatR;

namespace gRPCTemplate.Application.Requests.Common;

public class GetByIdQuery<TResponse> : IRequest<TResponse>
{
    public Guid Id { get; init; }

    public GetByIdQuery() { }
    public GetByIdQuery(Guid id)
    {
        Id = id;
    }
}
