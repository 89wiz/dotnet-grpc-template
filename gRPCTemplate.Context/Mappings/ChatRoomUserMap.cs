using gRPCTemplate.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace gRPCTemplate.Context.Mappings;

public class ChatRoomUserMap : IEntityTypeConfiguration<ChatRoomUser>
{
    public void Configure(EntityTypeBuilder<ChatRoomUser> builder)
    {
        builder.ToTable(nameof(ChatRoomUser))
            .HasKey(x => new { x.UserId, x.RoomId });

        builder.HasOne(x => x.User)
            .WithMany()
            .HasPrincipalKey(x => x.Id)
            .HasForeignKey(x => x.UserId);

        builder.HasOne(x => x.Room)
            .WithMany()
            .HasPrincipalKey(x => x.Id)
            .HasForeignKey(x => x.RoomId);
    }
}
