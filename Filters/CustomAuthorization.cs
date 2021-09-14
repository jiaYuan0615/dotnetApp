using System;
using System.Linq;
using dotnetApp.Helpers;
using dotnetApp.Services;
using Microsoft.AspNetCore.Mvc.Filters;

namespace dotnetApp.Filters
{
  public class CustomAuthorization : Attribute, IAuthorizationFilter
  {
    private readonly IMemberService _memberService;

    public CustomAuthorization(IMemberService memberService)
    {
      _memberService = memberService;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
      string id = context.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "id").Value;
      var member = _memberService.GetAssignMemberById(Guid.Parse(id));
      if (member == null) throw new NotFoundException("找不到該使用者");
      // pass data to next
      context.HttpContext.Items["data"] = member.email;
    }
  }
}
