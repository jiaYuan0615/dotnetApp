using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using dotnetApp.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace dotnetApp.Middlewares
{
  public class ErrorHandlerMiddleware
  {
    private readonly RequestDelegate _next;
    public ErrorHandlerMiddleware(RequestDelegate next)
    {
      _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
      try
      {
        await _next(context);
      }
      catch (Exception error)
      {
        var response = context.Response;
        response.ContentType = "application/json";

        switch (error)
        {
          case AppException e:
            response.StatusCode = (int)HttpStatusCode.BadRequest;
            break;
          case NotFoundException e:
            response.StatusCode = (int)HttpStatusCode.NotFound;
            break;
          default:
            response.StatusCode = (int)HttpStatusCode.InternalServerError;
            break;
        }
        var result = JsonSerializer.Serialize(new { message = error?.Message });
        // show message to response
        await response.WriteAsync(result);
      }
    }
  }

  public static class ErrorHandlerMiddlewareExtensions
  {
    public static IApplicationBuilder UseErrorHandlerMiddleware(this IApplicationBuilder builder)
    {
      return builder.UseMiddleware<ErrorHandlerMiddleware>();
    }
  }
}
