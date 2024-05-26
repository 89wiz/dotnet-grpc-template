using gRPCTemplate.Application.Common;
using gRPCTemplate.Context;
using gRPCTemplate.gRPC.Services;
using gRPCTemplate.IoC;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configuration = builder.Configuration;

services.AddGrpc(o => o.EnableDetailedErrors = true);
services.AddGrpcReflection();

services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Config.Assembly));

services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
{
    builder.WithOrigins("http://localhost:4200")
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials();
}));

services
    .AddAuthorization(opt =>
    {
        opt.FallbackPolicy = new AuthorizationPolicyBuilder()
            .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
            .RequireAuthenticatedUser()
            .Build();
    })
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(opt =>
    {
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)),
            ValidateIssuer = false,
            ValidateAudience = false,
            //ValidateIssuer = true,
            //ValidIssuer = builder.Configuration["Jwt:Issuer"],
            //ValidateAudience = true,
            //ValidAudience = builder.Configuration["Jwt:Audience"],
            ValidateLifetime = false
        };
    });

services.AddDbContext<MyContext>(opt =>
{
    opt.UseSqlite("Data Source=tasks.db", opt => { opt.MigrationsAssembly("gRPCTemplate.Context"); });
});

services.AddDependencyInjection();
//services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

//app.UseHttpsRedirection();

app.MapGrpcService<AuthService>()
    .RequireCors("CorsPolicy");//.AllowAnonymous();

app.MapGrpcReflectionService()
    .AllowAnonymous();

app.MapGet("/", () => "Hello World!");//.AllowAnonymous();

app.Run();
