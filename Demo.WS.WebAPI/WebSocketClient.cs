using System;
using System.Net.WebSockets;
using System.Text;

namespace Demo.WS.WebAPI
{
  public class WebSocketClient
  {
    public Guid ConnectionId { get; set; }
    public string UserName { get; set; }
    public WebSocket UserWebSocket { get; set; }

    public Task SendMessageAsync(string message)
    {
      var msg = Encoding.UTF8.GetBytes(message);
      return UserWebSocket.SendAsync(
        new ArraySegment<byte>(msg, 0, msg.Length),
        WebSocketMessageType.Text,
        true,
        CancellationToken.None);
    }
  }
}

