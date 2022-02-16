using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetApp.Context;
using dotnetApp.Dtos.Sound;
using dotnetApp.Models;
using Microsoft.EntityFrameworkCore;

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
      return _databaseContext.Sounds
                .AsNoTracking()
                .SingleOrDefault(o => o.id == id);
    }

    public List<Sound> GetSound()
    {
      List<Sound> sound = _databaseContext.Sounds
                          .AsNoTracking()
                          .OrderByDescending(x => x.createdAt)
                          .ToList();
      return sound;
    }
    public IEnumerable<Sound> GetSound(bool isCover)
    {
      IEnumerable<Sound> sound = _databaseContext.Sounds.Where(o => o.isCover == isCover).ToList();
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
