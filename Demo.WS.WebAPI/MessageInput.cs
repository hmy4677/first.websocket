using System;
namespace Demo.WS.WebAPI
{
  public class MessageInput
  {
    public string SendClientId { get; set; }
    public string ToUserId { get; set; }
    public string FromUserId { get; set; }
    public string MessageType { get; set; }
    public string MessageContent { get; set; }
    public string Method { get; set; }
  }
}
