using System;
namespace Demo.WS.WebAPI
{
  public class WebSocketClientCollection
  {
    public static List<WebSocketClient> _clients { get; set; } = new List<WebSocketClient>();

    public static void Add(WebSocketClient client)
    {
      _clients.Add(client);
    }
    public static void Remove(WebSocketClient client)
    {
      _clients.Remove(client);
    }
    public static WebSocketClient Get(Guid clientId)
    {
      var client = _clients.FirstOrDefault(p => p.ConnectionId == clientId);
      return client;
    }
    public static WebSocketClient GetUser(string userId)
    {
      var client = _clients.FirstOrDefault(p => p.UserName == userId);
      return client;
    }
  }
}

