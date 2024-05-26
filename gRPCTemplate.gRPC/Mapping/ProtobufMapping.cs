using Riok.Mapperly.Abstractions;

namespace gRPCTemplate.gRPC.Mapping;

[Mapper]
public static partial class ProtobufMapping
{
    [UserMapping]
    public static Google.Protobuf.WellKnownTypes.Timestamp ToProtobuf(DateTime dateTime) =>
        Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(dateTime.ToUniversalTime());


    [UserMapping]
    public static DateTime ToDateTime(Google.Protobuf.WellKnownTypes.Timestamp timestamp) =>
        timestamp.ToDateTime();
}
