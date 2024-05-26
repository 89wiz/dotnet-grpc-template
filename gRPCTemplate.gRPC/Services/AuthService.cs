using Grpc.Core;
using gRPCTemplate.gRPC.Mapping;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace gRPCTemplate.gRPC.Services;

public class AuthService(ISender sender) : Auth.AuthBase
{
    private readonly ISender sender = sender;

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