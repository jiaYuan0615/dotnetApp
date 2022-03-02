using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetApp.dotnetApp.Context;
using dotnetApp.dotnetApp.Dtos.Singer;
using dotnetApp.dotnetApp.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnetApp.dotnetApp.Services
{
  public class SingerService
  {
    private readonly DatabaseContext _databaseContext;

    public SingerService(DatabaseContext databaseContext)
    {
      _databaseContext = databaseContext;
    }

    public async Task DeleteSinger(Singer singer)
    {
      _databaseContext.Singers.Remove(singer);
      await _databaseContext.SaveChangesAsync();
    }

    public Singer GetAssignSinger(Guid id)
    {
      return _databaseContext.Singers.Where(x => x.id == id).FirstOrDefault();
    }

    public List<SingerGroup> GetSinger()
    {
      string sql = @"
      SELECT
        singers.id,
        singers.`name`,
        singers.avatar,
        singers.nickname,
        singers.gender,
        singers.birth,
        singers.biography,
        singers.country,
        `groups`.id AS groupId,
        `groups`.`name` AS groupName,
        `groups`.createdAt AS groupCreatedAt,
        `groups`.updatedAt AS groupupdatedAt
      FROM
        singers
        LEFT JOIN `groups` ON singers.groupId = `groups`.id";

      List<SingerGroup> SingerGroups = _databaseContext.SingerGroups
      .FromSqlRaw(sql)
      .AsNoTracking()
      .ToList();

      return SingerGroups;
    }

    public SingerSound GetSingerSong(Guid id)
    {
      FormattableString sql = $@"
      SELECT
        singers.id,
        singers.`name`,
        singers.nickname,
        singers.birth,
        singers.avatar,
        singers.gender,
        singers.country,
        singers.groupId,
        `groups`.`name` AS groupName,
        `sounds`.id AS soundId,
        `sounds`.`name` AS soundName,
        `sounds`.publishYear AS soundPublishYear
      FROM
        singers
        LEFT JOIN `groups` ON `groups`.id = singers.groupId
        LEFT JOIN ( singer_sound AS ss INNER JOIN `sounds` ON `sounds`.id = ss.soundId ) ON ss.singerId = singers.id
      WHERE
        singers.id = {id}";
      SingerSound singerSong = _databaseContext.SingerSongs
      .FromSqlInterpolated(sql)
      .AsNoTracking()
      .FirstOrDefault();
      return singerSong;
    }

    public async Task PostSinger(Singer singer)
    {
      _databaseContext.Singers.Add(singer);
      await _databaseContext.SaveChangesAsync();
    }

    public async Task UpdateSinger(Singer singer, SingerUpdate singerUpdate)
    {
      _databaseContext.Entry(singer).CurrentValues.SetValues(singerUpdate);
      await _databaseContext.SaveChangesAsync();
    }

    public async Task UpdateSinger()
    {
      await _databaseContext.SaveChangesAsync();
    }
  }
}
