using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetApp.Context;
using dotnetApp.Models;
using dotnetApp.Repositories.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace dotnetApp.Services
{

  public interface IMemberService
  {
    IEnumerable<Member> GetMember();
    Member GetAssignMember(Guid id);
    bool MemberLogin();
    Task RegisterMember(RegisterRepository registerRepository);
    bool CheckPassword(string plain, string hash);
    string HashPassword(string plain);
  }
  public class MemberService : IMemberService
  {
    private readonly DatabaseContext _databaseContext;
    public MemberService(DatabaseContext databaseContext)
    {
      _databaseContext = databaseContext;
    }

    public IEnumerable<Member> GetMember()
    {
      return _databaseContext.Members.ToList();
    }
    public Member GetAssignMember(Guid id)
    {
      return _databaseContext.Members.SingleOrDefault(o => o.id == id);
    }

    public async Task RegisterMember(RegisterRepository registerRepository)
    {
      _databaseContext.Add(registerRepository);
      await _databaseContext.SaveChangesAsync();
    }

    public Member YieldMockData()
    {
      Member member = new Member
      {
        id = Guid.NewGuid(),
        email = "heroyuans@gmail.com",
        password = "password",
        avatar = "avatar",
        name = "yuan",
        gender = "men",
        email_verified = DateTime.Now,
      };
      DateTime currentTime = DateTime.Now.AddHours(2);
      DateTime expireTime = DateTime.Now.AddHours(1);
      Int32 status = DateTime.Compare(currentTime, expireTime);
      return member;
    }

    public bool MemberLogin()
    {
      string mockEmail = "chenyan@gmail.com";
      string mockPassword = "hashedPassword";
      var member = _databaseContext.Members.SingleOrDefault(x => x.email == mockEmail);
      if (member == null || !this.CheckPassword(member.password, mockPassword))
      {
        return false;
      }
      return true;
    }

    public bool CheckPassword(string plain, string hash)
    {
      return BCrypt.Net.BCrypt.Verify(plain, hash);
    }

    public string HashPassword(string plain)
    {
      return BCrypt.Net.BCrypt.HashPassword(plain);
    }
  }
}
