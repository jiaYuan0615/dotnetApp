using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetApp.Context;
using dotnetApp.Dtos.Singer;
using dotnetApp.Models;

namespace dotnetApp.Services
{

  public interface ISingerService
  {
    IEnumerable<Singer> GetSinger();
    Singer GetAssignSinger(Guid id);
    Task PostSinger(Singer singer);
    Task UpdateSinger(Singer singer, SingerUpdate singerUpdate);
    Task DeleteSinger(Singer singer);
  }
  public class SingerService : ISingerService
  {
    private readonly DatabaseContext _databaseContext;

    public SingerService(DatabaseContext databaseContext)
    {
      _databaseContext = databaseContext;
    }

    public Task DeleteSinger(Singer singer)
    {
      throw new NotImplementedException();
    }

    public Singer GetAssignSinger(Guid id)
    {
      throw new NotImplementedException();
    }

    public IEnumerable<Singer> GetSinger()
    {
      return _databaseContext.Singers.ToList();
    }

    public async Task PostSinger(Singer singer)
    {
      _databaseContext.Singers.Add(singer);
      await _databaseContext.SaveChangesAsync();
    }

    public Task UpdateSinger(Singer singer, SingerUpdate singerUpdate)
    {
      throw new NotImplementedException();
    }
  }
}
