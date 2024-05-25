var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configuration = builder.Configuration;

//services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Config.Assembly));

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
