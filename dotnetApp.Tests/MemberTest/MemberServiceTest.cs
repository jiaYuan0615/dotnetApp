using dotnetApp.dotnetApp.Services;
using Microsoft.AspNetCore.Hosting;
using NUnit.Framework;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore;
using System.Collections.Generic;
using dotnetApp.dotnetApp.Models;
using System.Linq;
using System;
using System.IO;

namespace dotnetApp.Tests.MemberTest
{
  [TestFixture]
  public class MemberServiceTest
  {
    private MemberService _memberService;
    private PasswordService _passwordService;
    static IWebHost _webHost = null;

    static T GetService<T>()
    {
      IServiceScope scope = _webHost.Services.CreateScope();
      return scope.ServiceProvider.GetRequiredService<T>();
    }

    [SetUp]
    public void SetUp()
    {
      // 取得 DI 容器實體
      _webHost = WebHost.CreateDefaultBuilder()
                .UseStartup<Startup>()
                .Build();
      _memberService = GetService<MemberService>();
      _passwordService = GetService<PasswordService>();
    }

    [Test]
    public void Test_GetMember()
    {
      List<Member> member = _memberService.GetMember();
      Assert.IsNotNull(member);
    }

    [Test]
    public void Test_GetAssignMemberById()
    {
      Guid memberId = _memberService.GetMember().FirstOrDefault().id;
      Member member = _memberService.GetAssignMemberById(memberId);
      Assert.AreEqual(memberId, member.id);
      Assert.IsNotNull(member);
    }

  }
}