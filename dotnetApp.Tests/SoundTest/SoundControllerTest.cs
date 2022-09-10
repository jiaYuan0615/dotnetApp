using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using dotnetApp.dotnetApp.Dtos.Singer;
using dotnetApp.dotnetApp.Models;
using dotnetApp.dotnetApp.Services;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using static System.Net.Mime.MediaTypeNames;

namespace dotnetApp.Tests.SoundTest
{

  [TestFixture]
  public class SoundControllerTest
  {
    private TestServer Server;
    private HttpClient Client;
    private IConfiguration Configuration;

    [SetUp]
    public void SetUp()
    {
      // 模擬 httpClient 測試環境
      Configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                                                  .Build();
      IWebHostBuilder builder = new WebHostBuilder().UseEnvironment("Test")
                                                .UseConfiguration(Configuration)
                                                .UseStartup<Startup>();
      Server = new TestServer(builder);
      Client = Server.CreateClient();
    }

    [Test]
    public async Task Expect_200_TestGetSound()
    {
      string url = "api/sound";
      HttpResponseMessage response = await Client.GetAsync(url);
      Assert.True(response.IsSuccessStatusCode);
      Assert.AreEqual(200, (int)response.StatusCode);
    }

    // [Test]
    // public void Expect_400_TestPostSound()
    // {
    //   string url = "api/sound";
    //   // post form-data example
    //   // https://csharpkh.blogspot.com/2017/10/c-httpclient-webapi-6-post.html

    // }
  }
}