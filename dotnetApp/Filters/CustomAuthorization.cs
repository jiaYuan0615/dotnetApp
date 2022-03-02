using System;
using System.Linq;
using dotnetApp.dotnetApp.Helpers;
using dotnetApp.dotnetApp.Models;
using dotnetApp.dotnetApp.Services;
using Microsoft.AspNetCore.Mvc.Filters;

namespace dotnetApp.dotnetApp.Filters
{
  public class CustomAuthorization : Attribute, IAuthorizationFilter
  {
    private readonly MemberService _memberService;

    public CustomAuthorization(MemberService memberService)
    {
      _memberService = memberService;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
      string id = context.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "id").Value;
      Member member = _memberService.GetAssignMemberById(Guid.Parse(id));
      if (member == null) throw new NotFoundException("找不到該使用者");
      // Pass data to next
      context.HttpContext.Items["email"] = member.email;
    }
  }
}
