using gRPCTemplate.Application.Common;
using Riok.Mapperly.Abstractions;

namespace gRPCTemplate.Application.User.Register;

public class RegisterCommand : ICommand<RegisterResult>
{
    public string Login { get; set; }
    public string Nickname { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
}

public class RegisterResult
{
    public Guid Id { get; set; }
    public string Nickname { get; set; }
    public DateTime CreatedAt { get; set; }
}

[Mapper]
public static partial class RegisterMapper
{
    public static partial Domain.Entities.User AsEntity(this RegisterCommand command);
    public static partial RegisterResult AsResult(this Domain.Entities.User user);
}