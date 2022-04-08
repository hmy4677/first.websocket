using System;
using System.Net.WebSockets;
using System.Text;
using Newtonsoft.Json;

namespace Demo.WS.WebAPI
{
  public class WebSockethandlerMiddleware
  {
    private readonly RequestDelegate _next;
    private const string routePostfix = "/ws";

    public WebSockethandlerMiddleware(RequestDelegate next)
    {
      _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
      if (IsWebSocket(context))
      {
        var userName = context.Request.Query["userName"].ToArray()[0];
        var toUserName = context.Request.Query["toUserName"].ToArray()[0];

        using var webSocket = await context.WebSockets.AcceptWebSocketAsync();

        await Echo(webSocket, userName, toUserName);
      }
      else
      {
        await _next.Invoke(context);
      }
    }

    private static async Task Echo(WebSocket webSocket, string userName, string toUserName)
    {
      var client = new WebSocketClient
      {
        ConnectionId = Guid.NewGuid(),
        UserName = userName,
        UserWebSocket = webSocket
      };
      WebSocketClientCollection.Add(client);

      var buffer = new byte[1024 * 4];
      var receiveResult = await webSocket.ReceiveAsync(
          new ArraySegment<byte>(buffer), CancellationToken.None);

      while (!receiveResult.CloseStatus.HasValue)
      {
        var msg = Encoding.UTF8.GetString(buffer);
        var newMsg = userName + ':' + msg;

        await WebSocketClientCollection.GetUser(toUserName).UserWebSocket.SendAsync(
            new ArraySegment<byte>(Encoding.UTF8.GetBytes(newMsg), 0, userName.Length + 1 + receiveResult.Count),
            receiveResult.MessageType,
            receiveResult.EndOfMessage,
            CancellationToken.None);

        receiveResult = await webSocket.ReceiveAsync(
            new ArraySegment<byte>(buffer), CancellationToken.None);

      }

      await webSocket.CloseAsync(
          receiveResult.CloseStatus.Value,
          receiveResult.CloseStatusDescription,
          CancellationToken.None);
    }

    private static bool IsWebSocket(HttpContext context)
    {
      return context.WebSockets.IsWebSocketRequest &&
        context.Request.Path == routePostfix;
    }
  }
}