using System;
using Newtonsoft.Json;
using System.Collections.Generic;
namespace Demo.WS.WebAPI
{
  public static class JsonHelper
  {
    public static T ToObject<T>(this string json)
    {
      return json == null ? default(T) : JsonConvert.DeserializeObject<T>(json);
    }


  }

  public interface IJsonSerializerProvider
  {
    string Serialize(object value, object jsonSerializerOptions = default);
  }
}

