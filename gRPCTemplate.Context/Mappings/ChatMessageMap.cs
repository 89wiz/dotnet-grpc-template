using gRPCTemplate.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace gRPCTemplate.Context.Mappings;

public class ChatMessageMap : IEntityTypeConfiguration<ChatMessage>
{
    public void Configure(EntityTypeBuilder<ChatMessage> builder)
    {
        builder.ToTable(nameof(ChatMessage))
            .HasKey(x => new { x.RoomId, x.UserId, x.SentAt });

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
