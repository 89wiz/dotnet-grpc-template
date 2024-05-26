namespace gRPCTemplate.Domain.Entities;

public class ChatRoom
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid OwnerId { get; set; }
    public DateTime CreatedAt { get; set; }

    public virtual User Owner { get; set; }
}
