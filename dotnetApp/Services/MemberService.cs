using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetApp.dotnetApp.Context;
using dotnetApp.dotnetApp.Dtos.Member;
using dotnetApp.dotnetApp.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnetApp.dotnetApp.Services
{
  public class MemberService
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
    public async Task UpdateMember()
    {
      await _databaseContext.SaveChangesAsync();
    }
    public async Task UpdateMember(Member member, MemberUpdate memberUpdate)
    {
      _databaseContext.Entry(member).CurrentValues.SetValues(memberUpdate);
      await _databaseContext.SaveChangesAsync();
    }
    public async Task DeleteMember(Member member)
    {
      _databaseContext.Members.Remove(member);
      await _databaseContext.SaveChangesAsync();
    }

    public List<MemberRole> GetMemberRole(string email)
    {
      FormattableString sql = $@"
      SELECT
        members.id,
        members.email,
        members.`password`,
        roles.name
      FROM
        members
        LEFT JOIN ( member_role AS mr INNER JOIN roles ON roles.id = mr.roleId ) ON members.id = mr.memberId
      WHERE
        members.email = {email}";
      List<MemberRole> memberRoles = _databaseContext.MemberRoles
                                      .FromSqlInterpolated(sql)
                                      .AsNoTracking()
                                      .ToList();
      return memberRoles;
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
