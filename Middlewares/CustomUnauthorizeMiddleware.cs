using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace dotnetApp.Middlewares
{
  public class CustomUnauthorizeMiddleware
  {
    private RequestDelegate _next;

    public CustomUnauthorizeMiddleware(RequestDelegate next)
    {
      _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
      await _next(context);
      HttpResponse response = context.Response;
      int unauthorizedCode = (int)HttpStatusCode.Unauthorized;
      if (response.StatusCode == unauthorizedCode)
      {
        response.ContentType = "application/json";
        response.StatusCode = unauthorizedCode;
        string result = JsonSerializer.Serialize(new { message = "請於登入後進行" });
        await response.WriteAsync(result);
      }
    }
  }

  public static class CustomUnauthorizeMiddlewareExtensions
  {
    public static IApplicationBuilder UseCustomUnauthorizeMiddleware(this IApplicationBuilder builder)
    {
      return builder.UseMiddleware<CustomUnauthorizeMiddleware>();
    }
  }
}


