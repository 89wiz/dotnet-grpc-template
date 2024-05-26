using Grpc.Core;
using gRPCTemplate.Application.User.Login;
using gRPCTemplate.gRPC.Mapping;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace gRPCTemplate.gRPC.Services;

//[AllowAnonymous]
public class AuthService(ISender sender) : Auth.AuthBase
{
    private readonly ISender sender = sender;

    [AllowAnonymous]
    public override Task<EchoResponse> Echo(EchoRequest request, ServerCallContext context)
    {
        return Task.FromResult(new EchoResponse { Message = request.Message });
    }

    public override async Task AsyncEcho(IAsyncStreamReader<EchoRequest> requestStream, IServerStreamWriter<EchoResponse> responseStream, ServerCallContext context)
    {
        while (await requestStream.MoveNext())
        {
            await responseStream.WriteAsync(new EchoResponse { Message = requestStream.Current.Message });
        }
        return;
    }

    [AllowAnonymous]
    public override async Task<RegisterResponse> Register(RegisterRequest request, ServerCallContext context)
    {
        return (await sender.Send(request.AsCommand(), context.CancellationToken))
            .Match(
                success => new RegisterResponse { Success = success.AsResponse() },
                error => new RegisterResponse { Error = error.AsResponse() });
    }

    [AllowAnonymous]
    public override async Task<LoginResponse> Login(LoginRequest request, ServerCallContext context)
    {
        return (await sender.Send(request.AsCommand(), context.CancellationToken))
            .Match(
                success => new LoginResponse { Success = success.AsResponse() },
                error => new LoginResponse { Error = error.AsResponse() });
    }
}
