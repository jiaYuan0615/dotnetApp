using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dotnetApp.Context;
using dotnetApp.Helpers;
using dotnetApp.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
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
      services.AddDbContext<DatabaseContext>(options =>
      {
        string ConnectionString = Configuration.GetConnectionString("connectionStrings");
        options.UseMySql(ConnectionString, ServerVersion.AutoDetect(ConnectionString));
      });

      services.AddMvc().AddJsonOptions(
        options =>
        {
          options.JsonSerializerOptions.IgnoreNullValues = true;
        }
      );
      services.AddSingleton<JwtHelpers>();
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

      // 解決物件多層結構不支援的問題
      services.AddControllers().AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

      // 讓 Authorization 知道要讀取 bearer 開頭的 token
      services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
      .AddJwtBearer(options =>
      {
        // 設定true時，當驗證失敗，回應標頭會包含錯誤訊息
        options.IncludeErrorDetails = true;

        options.TokenValidationParameters = new TokenValidationParameters
        {
          // 宣告後即可透過 User.Identity.Name 取得 "sub" 的值
          NameClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name",
          ValidateIssuer = true,
          ValidIssuer = Configuration.GetValue<string>("JwtSettings:Issuer"),
          ValidateAudience = false,
          // 驗證有效時間
          ValidateLifetime = true,

          IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetValue<string>("JwtSettings:SignKey")))
        };
      });

      services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      // app.UseMemberMiddleware();

      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseHttpsRedirection();

      app.UseRouting();

      app.UseAuthentication();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
