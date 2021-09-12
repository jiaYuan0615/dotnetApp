using System;

namespace dotnetApp.Dtos.Sound
{
  public class SoundRead
  {
    public Guid id { get; set; }
    public string name { get; set; }
    public string lyrics { get; set; }
    public string publishYear { get; set; }
    public string cover { get; set; }
    public string OST { get; set; }
    public bool isCover { get; set; }
    public DateTime createdAt { get; set; }
    public DateTime updatedAt { get; set; }
  }
}
