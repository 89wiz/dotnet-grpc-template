using Riok.Mapperly.Abstractions;

namespace gRPCTemplate.gRPC.Mapping;

[Mapper]
public static partial class ErrorMapping
{
    public static partial ErrorMessage AsResponse(this Domain.Common.ErrorMessage error);
}
