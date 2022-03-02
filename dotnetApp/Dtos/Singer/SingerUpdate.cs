using System;

namespace dotnetApp.dotnetApp.Dtos.Singer
{
  public class SingerUpdate
  {
    public string name { get; set; }
    public string avatar { get; set; }
    public string biography { get; set; }
    public Guid groupId { get; set; }
    public string nickname { get; set; }
    public string country { get; set; }
  }
}
