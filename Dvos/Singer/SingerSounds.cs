using System.Collections.Generic;
using dotnetApp.Dtos.Sound;

namespace dotnetApp.Dtos.Singer
{
  public class SingerSounds
  {
    public string id { get; set; }
    public string name { get; set; }
    public string nickName { get; set; }
    public string gender { get; set; }
    public string avatar { get; set; }
    public string birth { get; set; }
    public string country { get; set; }
    public string groupId { get; set; }
    public string groupName { get; set; }
    public List<SoundList> sounds { get; set; }
  }
}
