namespace gRPCTemplate.Domain.Entities;

public class ChatRoomUser
{
    public Guid RoomId { get; set; }
    public Guid UserId { get; set; }

    public virtual ChatRoom Room { get; set; }
    public virtual User User { get; set; }
}
