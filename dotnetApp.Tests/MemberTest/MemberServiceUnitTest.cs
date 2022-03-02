using System.Collections.Generic;
using dotnetApp.dotnetApp.Services;
using dotnetApp.dotnetApp.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace dotnetApp.Tests.MemberTest
{
  [TestFixture]
  public class MemberServiceUnitTest
  {
    private MemberService _memberService;
    static IWebHost _webHost = null;
    static T GetService<T>()
    {
      var scope = _webHost.Services.CreateScope();
      return scope.ServiceProvider.GetRequiredService<T>();
    }

    [SetUp]
    public void SetUp()
    {
      _webHost = WebHost.CreateDefaultBuilder()
                .UseStartup<Startup>()
                .Build();

      _memberService = GetService<MemberService>();
    }

    [Test]
    public void TestGetMember()
    {
      List<Member> member = _memberService.GetMember();
      Assert.IsNotNull(member);
    }

    [Test]
    public void TestPostMember()
    {

    }
  }
}
