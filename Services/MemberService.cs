using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using dotnetApp.Context;
using dotnetApp.Dtos.Collection;
using dotnetApp.Dtos.Member;
using dotnetApp.Dvos.Member;
using dotnetApp.Helpers;
using dotnetApp.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnetApp.Services
{

  public interface IMemberService
  {
    List<Member> GetMember();
    Member GetAssignMemberById(Guid id);
    Task RegisterMember(Member member);
    Member GetAssignMemberByEmail(string email);
    Task UpdateMember(Member member, MemberUpdate memberUpdate);
    Task DeleteMember(Member member);
    List<Member> GetMemberCollectionItem(string id);
    List<MemberCollection> GetMemberCollection(string id);
    List<MemberCollection> GetMemberCollection();
  }
  public class MemberService : IMemberService
  {
    private readonly DatabaseContext _databaseContext;

    public MemberService(DatabaseContext databaseContext)
    {
      _databaseContext = databaseContext;
    }

    public List<Member> GetMember()
    {
      var member = _databaseContext.Members
                .Include(x => x.Collections)
                .ToList();
      return _databaseContext.Members.ToList();
    }
    public Member GetAssignMemberById(Guid id)
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

    public async Task UpdateMember(Member member, MemberUpdate memberUpdate)
    {
      _databaseContext.Entry(member).CurrentValues.SetValues(memberUpdate);
      await _databaseContext.SaveChangesAsync();
    }
    public async Task UpdateMember()
    {
      await _databaseContext.SaveChangesAsync();
    }
    public async Task DeleteMember(Member member)
    {
      _databaseContext.Members.Remove(member);
      await _databaseContext.SaveChangesAsync();
    }

    public Member GetAssignMemberByEmail(string email)
    {
      var member = _databaseContext.Members.Where(x => x.email == email).First();
      return member;
    }

    public List<Member> GetMemberCollectionItem(string id)
    {
      // using orm
      List<Member> member = _databaseContext.Members
      .Include(x => x.Collections)
      .ToList();
      return member;
    }

    public List<MemberCollection> GetMemberCollection(string id)
    {
      // using pure sql command
      string sql = $@"
      SELECT
        Members.id,
        Members.email,
        Members.`name`,
        Members.gender,
        Collections.id AS collectionId,
        Collections.`name` AS collectionName
      FROM
        Members
        LEFT JOIN Collections ON Collections.memberId = Members.id
      WHERE
        Members.id = '{id}'";
      var member = _databaseContext.MemberCollections
      .FromSqlRaw(sql)
      .ToList();
      return member;
    }

    public List<MemberCollection> GetMemberCollection()
    {
      string sql = $@"
      SELECT
        Members.id,
        Members.email,
        Members.`name`,
        Members.gender,
        Collections.id AS collectionId,
        Collections.`name` AS collectionName
      FROM
        Members
        LEFT JOIN Collections ON Collections.memberId = Members.id";
      var member = _databaseContext.MemberCollections
      .FromSqlRaw(sql)
      .ToList();
      return member;
    }
  }
}
