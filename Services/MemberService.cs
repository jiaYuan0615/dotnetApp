using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetApp.Context;
using dotnetApp.Models;
using dotnetApp.Repositories.User;
using Microsoft.EntityFrameworkCore;

namespace dotnetApp.Services
{
  public class MemberService
  {
    private readonly DatabaseContext _DatabaseContext;
    public MemberService(DatabaseContext DatabaseContext)
    {
      _DatabaseContext = DatabaseContext;
    }

    public IEnumerable<Member> GetMember()
    {
      return _DatabaseContext.Members.ToList();
    }

    public Member GetAssignMember(Guid id)
    {
      return _DatabaseContext.Members.SingleOrDefault(o => o.id == id);
    }

    public async Task RegisterMember(RegisterRepository registerRepository)
    {
      await _DatabaseContext.AddAsync(registerRepository);
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

    public async Task UserLogin()
    {
      string data = "data";
      _DatabaseContext.Add(data);
      await _DatabaseContext.SaveChangesAsync();
    }
  }
}
