using System;
using System.Collections.Generic;
using dotnetApp.dotnetApp.Dtos.Sound;

namespace dotnetApp.dotnetApp.Dvos.Collection
{
  public class CollectionSounds
  {
    public string id { get; set; }
    public string name { get; set; }
    public List<SoundList> sounds { get; set; }
    public string createdAt { get; set; }
  }
}
