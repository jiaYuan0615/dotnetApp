using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetApp.Context;
using dotnetApp.Dtos.Sound;
using dotnetApp.Models;

namespace dotnetApp.Services
{
  public class SoundService
  {
    private readonly DatabaseContext _databaseContext;
    public SoundService(DatabaseContext databaseContext)
    {
      _databaseContext = databaseContext;
    }

    public async Task<string> PostSound(Sound sound)
    {
      _databaseContext.Sounds.Add(sound);
      await _databaseContext.SaveChangesAsync();
      return sound.id.ToString();
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

    public List<Sound> GetSound()
    {
      List<Sound> sound = _databaseContext.Sounds.ToList();
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
