namespace gRPCTemplate.Domain.Entities;

public class ChatMessage
{
    public Guid RoomId { get; set; }
    public Guid UserId { get; set; }
    public string Message { get; set; }
    public DateTime SentAt { get; set; }

    public virtual ChatRoom Room { get; set; }
    public virtual User User { get; set; }
}
