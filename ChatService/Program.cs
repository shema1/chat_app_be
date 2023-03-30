using ChatService.Hubs;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddSignalR();

builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

builder.Services.AddSingleton<IDictionary<string, ChatService.UserConnection>>( opts => new Dictionary<string, ChatService.UserConnection>());

var app = builder.Build();

app.UseCors(builder => builder
.AllowAnyHeader()
.AllowAnyMethod()
.SetIsOriginAllowed((host) => true)
.AllowCredentials());


app.MapGet("/", () => "Hello World!");

app.MapHub<ChatHub>("/chat");

app.Run();

