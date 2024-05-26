using gRPCTemplate.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace gRPCTemplate.Context.Mappings;

public class ChatRoomMap : IEntityTypeConfiguration<ChatRoom>
{
    public void Configure(EntityTypeBuilder<ChatRoom> builder)
    {
        builder.ToTable(nameof(ChatRoom))
            .HasKey(x => x.Id);

        builder.HasOne(x => x.Owner)
            .WithMany()
            .HasPrincipalKey(x => x.Id)
            .HasForeignKey(x => x.OwnerId);
    }
}
