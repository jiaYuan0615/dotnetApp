using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetApp.Context;
using dotnetApp.Dtos.Singer;
using dotnetApp.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnetApp.Services
{

  public interface ISingerService
  {
    IEnumerable<Singer> GetSinger();
    Singer GetAssignSinger(Guid id);
    SingerSound GetSingerSong(Guid id);
    Task PostSinger(Singer singer);
    Task UpdateSinger(Singer singer, SingerUpdate singerUpdate);
    Task UpdateSinger();
    Task DeleteSinger(Singer singer);
  }
  public class SingerService : ISingerService
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

    public IEnumerable<Singer> GetSinger()
    {
      return _databaseContext.Singers.ToList();
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
