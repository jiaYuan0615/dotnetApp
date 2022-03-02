using System.Collections.Generic;
using dotnetApp.dotnetApp.Dvos.Sound;

namespace dotnetApp.dotnetApp.Dvos.Collection
{
  public class CollectionItems
  {
    public string id { get; set; }
    public string name { get; set; }
    public List<SoundItems> sounds { get; set; }
  }
}
