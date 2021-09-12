using System;

namespace dotnetApp.Dtos.Sound
{
  public class SoundUpdate
  {
    public string name { get; set; }
    public string lyrics { get; set; }
    public string publishYear { get; set; }
    public string cover { get; set; }
    public string OST { get; set; }
    public bool isCover { get; set; }
  }
}
