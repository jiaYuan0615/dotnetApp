
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace dotnetApp.Middlewares
{
  public class CustomForbiddenMiddleware
  {
    private RequestDelegate _next;

    public CustomForbiddenMiddleware(RequestDelegate next)
    {
      _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
      await _next(context);
      HttpResponse response = context.Response;
      int forbiddenCode = (int)HttpStatusCode.Forbidden;
      if (response.StatusCode == forbiddenCode)
      {
        response.ContentType = "application/json";
        response.StatusCode = forbiddenCode;
        string result = JsonSerializer.Serialize(new { message = "權限不符，無法進行此操作" });
        await response.WriteAsync(result);
      }
    }
  }


  public static class CustomForbiddenMiddlewareExtensions
  {
    public static IApplicationBuilder UseCustomForbiddenMiddleware(this IApplicationBuilder builder)
    {
      return builder.UseMiddleware<CustomForbiddenMiddleware>();
    }
  }
}
