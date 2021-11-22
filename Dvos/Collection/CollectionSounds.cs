using System.Collections.Generic;
using dotnetApp.Dtos.Sound;

namespace dotnetApp.Dvos.Collection
{
  public class CollectionSounds
  {
    public string id { get; set; }
    public string name { get; set; }
    public List<SoundList> sounds { get; set; }
    public string createdAt { get; set; }
  }
}
