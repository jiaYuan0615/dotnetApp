using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace dotnetApp.Helpers
{
  public class JwtHelpers
  {
    private readonly IConfiguration _configuration;

    public JwtHelpers(IConfiguration configuration)
    {
      _configuration = configuration;
    }

    public string yieldToken(string id, int expireHour = 24)
    {
      // 直接取值
      // var issuer = _configuration["JwtSettings:Issuer"];
      // var signKey = _configuration["JwtSettings:SignKey"];
      // 使用泛型取值
      string issuer = _configuration.GetValue<string>("JwtSettings:Issuer");
      string signKey = _configuration.GetValue<string>("JwtSettings:SignKey");
      string sub = _configuration.GetValue<string>("JwtSettings:Sub");

      List<Claim> claims = new List<Claim>();

      claims.Add(new Claim(JwtRegisteredClaimNames.Sub, sub));

      // JWT 的唯一識別碼
      claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

      claims.Add(new Claim("id", id));

      var identify = new ClaimsIdentity(claims);

      var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signKey));

      var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Issuer = issuer,
        Subject = identify,
        Expires = DateTime.Now.AddHours(expireHour),
        SigningCredentials = signingCredentials
      };
      var tokenHandler = new JwtSecurityTokenHandler();
      var securityToken = tokenHandler.CreateToken(tokenDescriptor);
      var token = tokenHandler.WriteToken(securityToken);

      return token;
    }
  }
}
