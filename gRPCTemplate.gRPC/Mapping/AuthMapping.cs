using gRPCTemplate.Application.Common.Results;
using gRPCTemplate.Application.User.Login;
using gRPCTemplate.Application.User.Register;
using Riok.Mapperly.Abstractions;

namespace gRPCTemplate.gRPC.Mapping;

[Mapper]
[UseStaticMapper(typeof(ProtobufMapping))]
public static partial class AuthMapping
{
    public static partial LoginCommand AsCommand(this LoginRequest request);
    public static partial LoginSuccessResponse AsResponse(this LoginResult result);

    public static partial RegisterCommand AsCommand(this RegisterRequest request);
    public static partial RegisterSuccessResponse AsResponse(this RegisterResult result);
}
