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
    Task RegisterMember(Member member);
    Task UpdateMember(Guid id, Member member);
    Task DeleteMember(Guid id);
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

    public async Task RegisterMember(Member member)
    {
      _databaseContext.Members.Add(member);
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

    public Task UpdateMember(Member member)
    {
      throw new NotImplementedException();
    }

    public Task UpdateMember(Guid id, Member member)
    {
      throw new NotImplementedException();
    }

    public Task DeleteMember(Guid id)
    {
      throw new NotImplementedException();
    }
  }
}
