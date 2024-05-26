using gRPCTemplate.Application.Responses.Common;
using gRPCTemplate.Domain.Common;
using MediatR;
using OneOf;

namespace gRPCTemplate.Application.Common;

public interface ICommand<TResponse> : IRequest<OneOf<TResponse, ErrorMessage>> where TResponse : class, new() { }
public interface IQuery<TResponse> : IRequest<TResponse> where TResponse : class, new() { }
public interface IUpdateCommand<TResponse> : ICommand<TResponse>
    where TResponse : class, new()
{
    public Guid Id { get; set; }
}
public interface IDeleteCommand : ICommand<DeleteResponse>
{
    public Guid Id { get; set; }
}

public interface ICommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, OneOf<TResponse, ErrorMessage>>
    where TCommand : ICommand<TResponse>
    where TResponse : class, new()
{ }