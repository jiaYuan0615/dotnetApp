using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using dotnetApp.dotnetApp.Dtos.Member;
using dotnetApp.dotnetApp.Services;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using NUnit.Framework;
using static System.Net.Mime.MediaTypeNames;

namespace dotnetApp.Tests.MemberTest
{
  [TestFixture]
  public class MemberServiceUnitTest
  {
    private MemberService _memberService;
    private PasswordService _passwordService;
    static IWebHost _webHost = null;

    private TestServer Server;
    private HttpClient Client;
    private IConfiguration Configuration;
    private string token = "";

    static T GetService<T>()
    {
      IServiceScope scope = _webHost.Services.CreateScope();
      return scope.ServiceProvider.GetRequiredService<T>();
    }

    [SetUp]
    public async Task SetUp()
    {
      _webHost = WebHost.CreateDefaultBuilder()
                .UseStartup<Startup>()
                .Build();
      _memberService = GetService<MemberService>();
      _passwordService = GetService<PasswordService>();

      // 模擬 httpClient 測試環境
      Configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                                                  .Build();
      IWebHostBuilder builder = new WebHostBuilder().UseEnvironment("Test")
                                                .UseConfiguration(Configuration)
                                                .UseStartup<Startup>();
      Server = new TestServer(builder);
      Client = Server.CreateClient();

      // 測試前先登入拿 token
      string url = "api/member/login";
      MemberLogin memberLogin = new MemberLogin
      {
        email = "heroyuans@gmail.com",
        password = "password"
      };
      StringContent item = new StringContent(
        JsonConvert.SerializeObject(memberLogin),
        Encoding.UTF8,
        Application.Json
      );

      HttpResponseMessage response = await Client.PostAsync(url, item);
      string content = await response.Content.ReadAsStringAsync();
      Login responseData = JsonConvert.DeserializeObject<Login>(content);
      this.token = responseData.token;
    }

    [Test]
    public async Task Expext_200_TestGetMember()
    {
      string url = "api/member";
      // 兩種皆可
      // Client.DefaultRequestHeaders.Add("Authorization", $"bearer {this.token}");
      Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", this.token);
      HttpResponseMessage response = await Client.GetAsync(url);
      // string authorization = response.RequestMessage.Headers.Authorization.ToString();
      Console.WriteLine(this.token);
      Assert.True(response.IsSuccessStatusCode);
      Assert.AreEqual(200, (int)response.StatusCode);
    }

    [Test]
    public async Task Expect_401_TestGetMember()
    {
      string url = "api/member";

      HttpResponseMessage response = await Client.GetAsync(url);
      // List<Member> member = _memberService.GetMember();
      // Assert.IsNotNull(member);
      Assert.False(response.IsSuccessStatusCode);
      Assert.AreEqual(401, (int)response.StatusCode);
    }

    [TestCase("heroyuans@gmail.com", "password")]
    public async Task Expect_200_TestMemberLogin(string email, string password)
    {
      string url = "api/member/login";
      MemberLogin memberLogin = new MemberLogin
      {
        email = email,
        password = password
      };
      StringContent item = new StringContent(
        JsonConvert.SerializeObject(memberLogin),
        Encoding.UTF8,
        Application.Json
      );
      HttpResponseMessage response = await Client.PostAsync(url, item);
      string content = await response.Content.ReadAsStringAsync();
      Login responseData = JsonConvert.DeserializeObject<Login>(content);
      Assert.AreEqual(200, (int)response.StatusCode);
      Assert.AreEqual("登入成功", responseData.message);
      Assert.IsNotNull(responseData.token);
    }

    [Test]
    public void Expect_200_TestRegisterMember()
    {
    }

    [TestCase()]
    public void Expect_200_TestResetMemberPassword()
    {

    }
  }

  public class Login
  {
    public string message { get; set; }
    public string token { get; set; }
  }
}
