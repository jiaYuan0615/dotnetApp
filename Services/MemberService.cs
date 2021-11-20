using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetApp.Context;
using dotnetApp.Dtos.Member;
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
      FormattableString sql = $@"
      SELECT
        members.id,
        members.email,
        members.`name`,
        members.gender,
        collections.id AS collectionId,
        collections.`name` AS collectionName
      FROM
        members
        LEFT JOIN collections ON collections.memberId = members.id
      WHERE
        members.id = {id}";
      List<MemberCollection> member = _databaseContext.MemberCollections
      .FromSqlInterpolated(sql)
      .AsNoTracking()
      .ToList();
      return member;
    }

    public List<MemberCollection> GetMemberCollection()
    {
      string sql = $@"
      SELECT
        members.id,
        members.email,
        members.`name`,
        members.gender,
        collections.id AS collectionId,
        collections.`name` AS collectionName
      FROM
        members
        LEFT JOIN collections ON collections.memberId = members.id";
      List<MemberCollection> member = _databaseContext.MemberCollections
      .FromSqlRaw(sql)
      .AsNoTracking()
      .ToList();
      return member;
    }
  }
}
