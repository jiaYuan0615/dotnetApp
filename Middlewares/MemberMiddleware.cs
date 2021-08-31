using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace dotnetApp.Middlewares
{
  public class MemberMiddleware : Attribute
  {
    private readonly RequestDelegate _next;
    public MemberMiddleware(RequestDelegate next)
    {
      _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
      context.Items["data"] = "passData";
      await _next(context);
    }
  }

  public static class MemberMiddlewareExtensions
  {
    public static IApplicationBuilder UseMemberMiddleware(this IApplicationBuilder builder)
    {
      return builder.UseMiddleware<MemberMiddleware>();
    }
  }
}
