using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetApp.Context;
using dotnetApp.Dtos.Sound;
using dotnetApp.Models;

namespace dotnetApp.Services
{
  public interface ISoundService
  {
    Sound GetAssignSound(Guid id);
    IEnumerable<Sound> GetSound();
    Task PostSound(Sound sound);
    Task UpdateSound(Sound sound, SoundUpdate soundUpdate);
    Task DeleteSound(Sound sound);
  }
  public class SoundService : ISoundService
  {
    private readonly DatabaseContext _databaseContext;
    public SoundService(DatabaseContext databaseContext)
    {
      _databaseContext = databaseContext;
    }

    public async Task PostSound(Sound sound)
    {
      _databaseContext.Sounds.Add(sound);
      await _databaseContext.SaveChangesAsync();
    }

    public async Task DeleteSound(Sound sound)
    {
      _databaseContext.Sounds.Remove(sound);
      await _databaseContext.SaveChangesAsync();
    }

    public Sound GetAssignSound(Guid id)
    {
      var sound = _databaseContext.Sounds.SingleOrDefault(o => o.id == id);
      return sound;
    }

    public IEnumerable<Sound> GetSound()
    {
      var sound = _databaseContext.Sounds.ToList();
      return sound;
    }
    public IEnumerable<Sound> GetSound(bool isCover)
    {
      var sound = _databaseContext.Sounds.Where(o => o.isCover == isCover).ToList();
      return sound;
    }

    public async Task UpdateSound()
    {
      await _databaseContext.SaveChangesAsync();
    }

    public async Task UpdateSound(Sound sound, SoundUpdate soundUpdate)
    {
      _databaseContext.Entry(sound).CurrentValues.SetValues(soundUpdate);
      await _databaseContext.SaveChangesAsync();
    }
  }
}
