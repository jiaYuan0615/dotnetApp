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
      var response = context.Response;
      if (response.StatusCode == (int)HttpStatusCode.Unauthorized)
      {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
        var result = JsonSerializer.Serialize(new { message = "請於登入後進行" });
        await context.Response.WriteAsync(result);
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


