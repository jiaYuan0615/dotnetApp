using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using dotnetApp.dotnetApp.Dtos.Member;
using dotnetApp.dotnetApp.Dtos.Singer;
using dotnetApp.dotnetApp.Services;
using dotnetApp.Tests.MemberTest;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using NUnit.Framework;
using static System.Net.Mime.MediaTypeNames;

namespace dotnetApp.Tests.SingerTest
{
  [TestFixture]
  public class SingerControllerTest
  {
    private TestServer Server;
    private HttpClient Client;
    private IConfiguration Configuration;
    static IWebHost _webHost = null;
    private GroupService _groupService;
    private string token = "";

    static T GetService<T>()
    {
      IServiceScope scope = _webHost.Services.CreateScope();
      return scope.ServiceProvider.GetRequiredService<T>();
    }

    [SetUp]
    public async Task SetUp()
    {
      // 取得 DI 容器實體
      _webHost = WebHost.CreateDefaultBuilder()
                .UseStartup<Startup>()
                .Build();
      _groupService = GetService<GroupService>();
      // 模擬 httpClient 測試環境
      Configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                                                  .Build();
      IWebHostBuilder builder = new WebHostBuilder().UseEnvironment("Development")
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
    public async Task Expect_200_PostSinger()
    {
      string url = "api/singer";
      Guid groupId = _groupService.GetGroup().FirstOrDefault().id;
      var payload = new SingerCreate
      {
        name = "測試",
        biography = "測試自我介紹",
        groupId = groupId,
        nickname = "測試暱稱",
        gender = "男性",
        birth = DateTime.Now,
        country = "測試國家"
      };

      Dictionary<string, string> formData = new Dictionary<string, string>()
      {
        {nameof(SingerCreate.name), "測試"},
        {nameof(SingerCreate.biography), "測試自我介紹"},
        {nameof(SingerCreate.groupId), groupId.ToString()},
        {nameof(SingerCreate.nickname), "測試暱稱"},
        {nameof(SingerCreate.gender),  "男性"},
        {nameof(SingerCreate.birth), DateTime.Now.ToString()},
        {nameof(SingerCreate.country), "測試國家"},
      };
      // get target image
      // string image = "/Users/zhangjiayuan/Desktop/SideProject/dotnetApp/dotnetApp/wwwroot/storage/404.png";
      // string path = Path.GetFullPath(image);
      // string replace = Path.GetRelativePath("../../../", Directory.GetCurrentDirectory());
      // string target = path.Replace(replace, "");
      FileStream stream = File.OpenRead("404.png");
      HttpResponseMessage response = null;
      StreamContent image = new StreamContent(stream);
      image.Headers.ContentType = new MediaTypeHeaderValue("image/png");
      // make image ContentType
      // ByteArrayers.ContentType = new MediaTypeHeaderValue("image/png");
      using (var content = new MultipartFormDataContent())
      {
        // data.Headers.ContentType = new MediaTypeHeaderValue("image/png");
        // Error Message : Read-only file system
        content.Add(image, "avatar", "404.png");
        // content.Add(data, "image", Path.GetFileName(target));
        // JSON 才能用這種方式新增
        // content.Add(new StringContent(
        //   JsonConvert.SerializeObject(payload),
        //   Encoding.UTF8,
        //   Application.Json
        // ));
        foreach (var item in formData)
        {
          // formdata 要用這種方式新增
          content.Add(new StringContent(item.Value), item.Key);
        }
        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", this.token);
        response = await Client.PostAsync(url, content);
        // content.Dispose();
      }
      Console.OutputEncoding = Encoding.UTF8;
      Console.WriteLine(response.Content.ReadAsStringAsync().Result);
      Assert.AreEqual(200, (int)response.StatusCode);
      Assert.True(response.IsSuccessStatusCode);
    }
  }
}