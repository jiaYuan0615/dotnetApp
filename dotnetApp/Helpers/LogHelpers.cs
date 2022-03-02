using System;
using System.Text.Json;

namespace dotnetApp.dotnetApp.Helpers
{
  public class LogHelpers
  {
    public static void consoleLog(object data)
    {
      JsonSerializerOptions options = new JsonSerializerOptions
      {
        WriteIndented = true,
        Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
      };
      string json = JsonSerializer.Serialize(data, options);
      Console.WriteLine(json);
    }
  }
}
