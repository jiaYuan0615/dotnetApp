using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using dotnetApp.Context;
using dotnetApp.Models;

namespace dotnetApp.Services
{
  public interface ISoundService
  {
    Sound GetAssignSound(Guid id);
    IEnumerable<Sound> GetSound();
    Task CreateSound(Sound sound);
    Task UpdateSound(Guid id, Sound sound);
    Task DeleteSound(Guid id);
  }
  public class SoundService : ISoundService
  {
    private readonly DatabaseContext _databaseContext;
    public SoundService(DatabaseContext databaseContext)
    {
      _databaseContext = databaseContext;
    }

    public async Task CreateSound(Sound sound)
    {
      _databaseContext.Sounds.Add(sound);
      await _databaseContext.SaveChangesAsync();
    }

    public Task DeleteSound(Guid id)
    {
      throw new NotImplementedException();
    }

    public Sound GetAssignSound(Guid id)
    {
      throw new NotImplementedException();
    }

    public IEnumerable<Sound> GetSound()
    {
      throw new NotImplementedException();
    }

    public Task UpdateSound(Guid id, Sound sound)
    {
      throw new NotImplementedException();
    }
  }
}
