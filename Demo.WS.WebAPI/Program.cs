using System.Net.WebSockets;
using System.Text;
using Demo.WS.WebAPI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

var wsOption = new WebSocketOptions
{
  KeepAliveInterval = TimeSpan.FromSeconds(120)
};
wsOption.AllowedOrigins.Add("http://192.168.1.2:8000");
wsOption.AllowedOrigins.Add("http://localhost:8000");
app.UseWebSockets(wsOption);
app.UseMiddleware<WebSockethandlerMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();

