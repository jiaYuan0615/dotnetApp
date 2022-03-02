using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace dotnetApp
{
  public class Program
  {
    public static void Main(string[] args)
    {
      CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureLogging(logging =>
            {
              // logging.ClearProviders();
              // logging.SetMinimumLevel(LogLevel.Warning);
            })
            .ConfigureWebHostDefaults(webBuilder =>
            {
              // webBuilder.UseStartup<Startup>().UseNLog();
              webBuilder.UseStartup<Startup>();
            });
  }
}
