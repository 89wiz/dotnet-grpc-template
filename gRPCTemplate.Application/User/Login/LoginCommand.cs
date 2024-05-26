using gRPCTemplate.Application.Common;
using gRPCTemplate.Application.Common.Results;
using MediatR;

namespace gRPCTemplate.Application.User.Login;

public class LoginCommand : ICommand<LoginResult>
{
    public string Login { get; set; }
    public string Password { get; set; }
}
public class LoginResult
{
    public string Token { get; set; }
}