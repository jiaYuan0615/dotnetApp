using System;

namespace dotnetApp.Dtos.Singer
{
  public class SingerRead
  {
    public Guid id { get; set; }
    public string name { get; set; }
    public string avatar { get; set; }
    public string biography { get; set; }
    public Guid groupId { get; set; }
    public string nickname { get; set; }
    public string gender { get; set; }
    public DateTime birth { get; set; }
    public string country { get; set; }
  }
}
