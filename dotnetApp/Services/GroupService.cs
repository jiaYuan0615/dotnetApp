using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetApp.dotnetApp.Context;
using dotnetApp.dotnetApp.Dtos.Group;
using dotnetApp.dotnetApp.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnetApp.dotnetApp.Services
{
  public class GroupService
  {
    private readonly DatabaseContext _databaseContext;
    public GroupService(DatabaseContext databaseContext)
    {
      _databaseContext = databaseContext;

    }
    public async Task DeleteGroup(Group group)
    {
      _databaseContext.Remove(group);
      await _databaseContext.SaveChangesAsync();
    }

    public List<GroupSinger> GetAssginGroupSingers(string id)
    {
      FormattableString sql = $@"
      SELECT
        `groups`.id,
        `groups`.`name`,
        singers.id AS singerId,
        singers.`name` AS singerName,
        singers.avatar AS singerAvatarm,
        singers.nickname AS singerNickname,
        singers.gender AS singerGender,
        singers.birth AS singerBirth,
        singers.country AS singerCountry
      FROM
        `groups`
        LEFT JOIN singers ON singers.groupId = `groups`.id
      WHERE
        `groups`.id = {id}";
      List<GroupSinger> groupSinger = _databaseContext.GroupSingers
      .FromSqlInterpolated(sql)
      .AsNoTracking()
      .ToList();

      return groupSinger;
    }

    public Group GetAssignGroup(Guid id)
    {
      return _databaseContext.Groups.Find(id);
    }

    public List<Group> GetGroup()
    {
      return _databaseContext.Groups.AsNoTracking().ToList();
    }

    public List<GroupSinger> GetGroupSingers()
    {
      string sql = $@"
      SELECT
        `groups`.id,
        `groups`.`name`,
        singers.id AS singerId,
        singers.`name` AS singerName,
        singers.avatar AS singerAvatarm,
        singers.nickname AS singerNickname,
        singers.gender AS singerGender,
        singers.birth AS singerBirth,
        singers.country AS singerCountry
      FROM
        `groups`
        LEFT JOIN singers ON singers.groupId = `groups`.id";

      List<GroupSinger> groupSinger = _databaseContext.GroupSingers
      .FromSqlRaw(sql)
      .AsNoTracking()
      .ToList();

      return groupSinger;
    }

    public async Task PostGroup(Group group)
    {
      _databaseContext.Groups.Add(group);
      await _databaseContext.SaveChangesAsync();
    }

    public async Task UpdateGroup(Group group, GroupUpdate groupUpdate)
    {
      _databaseContext.Entry(group).CurrentValues.SetValues(groupUpdate);
      await _databaseContext.SaveChangesAsync();
    }

    public async Task UpdateGroup()
    {
      await _databaseContext.SaveChangesAsync();
    }

    public async Task DeleteGroup(string id)
    {
      FormattableString sql = $@"DELETE from `groups` where `groups`.id = {id}";
      await _databaseContext.Database.ExecuteSqlInterpolatedAsync(sql);
    }
  }
}
