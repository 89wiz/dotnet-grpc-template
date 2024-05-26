using gRPCTemplate.Application.Common;
using gRPCTemplate.Application.Common.Results;
using gRPCTemplate.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OneOf;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace gRPCTemplate.Application.User.Login;

public class LoginHandler(IConfiguration configuration, IUnitOfWork unitOfWork)
    : ICommandHandler<LoginCommand, LoginResult>
{
    private readonly IConfiguration _configuration = configuration;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<OneOf<LoginResult, ErrorMessage>> Handle(LoginCommand command, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.DbSet<Domain.Entities.User>().FirstOrDefaultAsync(x => x.Login == command.Login);

        if (user == null || !BCrypt.Net.BCrypt.Verify(command.Password, user.Password))
            return new ErrorMessage { ErrorCode = 1, Message = "Invalid Login/Password"};

        var key = Encoding.ASCII.GetBytes(_configuration.GetSection("Jwt:Key").Value!);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = GetClaims(user),
            Expires = DateTime.UtcNow.AddMinutes(30),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var jwtToken = tokenHandler.WriteToken(token);

        return new LoginResult { Token = jwtToken };
    }

    private static ClaimsIdentity GetClaims(Domain.Entities.User user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Sid, user.Id.ToString()),
            new(ClaimTypes.Name, user.Nickname),
        };

        return new ClaimsIdentity(claims);
    }
}
