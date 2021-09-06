using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using dotnetApp.Context;
using dotnetApp.Controllers;
using dotnetApp.Helpers;
using dotnetApp.Middlewares;
using dotnetApp.Services;
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
using Microsoft.OpenApi.Models;
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


      services.AddSwaggerGen(option =>
      {
        option.SwaggerDoc("v1", new OpenApiInfo { Title = "歌曲推薦系統", Version = "v1" });
        var FilePath = Path.Combine(AppContext.BaseDirectory, "dotnetApp.xml");
        option.IncludeXmlComments(FilePath);
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
          .WithMethods("GET", "POST", "PATCH", "DELETE")
          .SetIsOriginAllowed(origin => true)
          .AllowCredentials();
        });
      });

      // 解決物件多層結構不支援的問題
      services.AddControllers().AddNewtonsoftJson(x =>
      {
        x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
      });

      // 讓 Authorization 知道要讀取 bearer 開頭的 token
      services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
      .AddJwtBearer(options =>
      {
        // 設定true時，當驗證失敗，回應標頭會包含錯誤訊息
        options.IncludeErrorDetails = true;

        options.TokenValidationParameters = new TokenValidationParameters
        {
          ValidateIssuer = true,
          ValidIssuer = Configuration.GetValue<string>("JwtSettings:Issuer"),
          ValidateAudience = false,
          // 驗證有效時間
          // 依照 Expires
          ClockSkew = TimeSpan.Zero,
          // 依照 Expires 並加上 5 分鐘
          // ValidateLifetime = true,

          IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetValue<string>("JwtSettings:SignKey")))
        };
      });

      services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
      services.AddScoped<IMemberService, MemberService>();
      services.AddScoped<ISoundService, SoundService>();
      services.AddScoped<IPasswordService, PasswordService>();
      services.AddScoped<IMailService, MailService>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      // app.UseMemberMiddleware();

      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseHsts();
      }

      app.UseSwagger();
      app.UseSwaggerUI(v =>
      {
        v.SwaggerEndpoint("v1/swagger.json", "My API V1");
      });

      app.UseHttpsRedirection();

      app.UseErrorHandlerMiddleware();

      app.UseCors();

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
