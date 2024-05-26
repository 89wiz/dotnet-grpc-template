namespace gRPCTemplate.Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Login { get; set; }
    public string Nickname { get; set; }
    public string Password { get; set; }
    public DateTime CreatedAt { get; set; }
}
