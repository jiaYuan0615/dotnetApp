using System;

namespace dotnetApp.dotnetApp.Dtos.Singer
{
  public class SingerRead
  {
    public string id { get; set; }
    public string name { get; set; }
    public string avatar { get; set; }
    public string nickname { get; set; }
    public string biography { get; set; }
    public string gender { get; set; }
    public DateTime birth { get; set; }
    public string country { get; set; }
  }
}
