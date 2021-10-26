using System;
using System.IO;
using System.Text;
using dotnetApp.Context;
using dotnetApp.Helpers;
using dotnetApp.Middlewares;
using dotnetApp.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

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
          .AllowAnyMethod()
          .AllowAnyOrigin();
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
        // 客製化 401 回應的其中一種方法
        // options.Events = new JwtBearerEvents
        // {
        //   OnChallenge = async (context) =>
        // {
        //   context.HandleResponse();
        //   context.Response.ContentType = "application/json";
        //   if (context.AuthenticateFailure != null)
        //   {
        //     context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
        //     var response = JsonSerializer.Serialize(new { message = "請於登入後進行" });
        //     await context.Response.WriteAsync(response);
        //   }
        // }
        // };
      });
      // services.AddScoped(typeof(CustomAuthorization));
      services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
      services.AddScoped<IMemberService, MemberService>();
      services.AddScoped<ICollectionService, CollectionService>();
      services.AddScoped<ISingerService, SingerService>();
      services.AddScoped<IGroupService, GroupService>();
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
      app.UseSwaggerUI(v => v.SwaggerEndpoint("v1/swagger.json", "My API V1"));

      app.UseHttpsRedirection();

      app.UseCors("CorsPolicy");

      app.UseRouting();

      // 客製化 401 回應的其中一種方法
      // 由於是覆寫原本的中介層 所以要放在驗證之前
      app.UseCustomUnauthorizeMiddleware();

      app.UseAuthentication();

      app.UseAuthorization();

      app.UseErrorHandlerMiddleware();
      app.UseEndpoints(endpoints => endpoints.MapControllers());
    }
  }
}
