using System;
using System.Collections.Generic;
using dotnetApp.Context;
using dotnetApp.Models;

namespace dotnetApp.Services
{
  public interface ISoundService
  {
    Sound GetAssignSound(Guid id);
    IEnumerable<Sound> GetSound();
  }
  public class SoundService : ISoundService
  {
    private readonly DatabaseContext _DatabaseContext;
    public SoundService(DatabaseContext DatabaseContext)
    {
      _DatabaseContext = DatabaseContext;
    }

    public Sound GetAssignSound(Guid id)
    {
      throw new NotImplementedException();
    }

    public IEnumerable<Sound> GetSound()
    {
      throw new NotImplementedException();
    }
  }
}
