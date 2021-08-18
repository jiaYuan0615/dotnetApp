using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using dotnetApp.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace dotnetApp
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddEntityFrameworkSqlServer().AddDbContext<DatabaseContext>(options =>
      {
        options.UseSqlServer(Configuration.GetConnectionString("connectionStrings"));
      }, ServiceLifetime.Scoped);

      services.AddMvc().AddJsonOptions(
        options =>
        {
          options.JsonSerializerOptions.IgnoreNullValues = true;
        }
      );

      services.AddHttpClient();
      services.AddCors(options =>
      {
        options.AddPolicy("CorsPolicy", policy =>
         {
           policy.AllowAnyHeader()
                 .AllowAnyMethod()
                 .AllowAnyMethod()
                 .AllowCredentials();
         });
      });

      services.AddControllers();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseHttpsRedirection();

      app.UseRouting();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
