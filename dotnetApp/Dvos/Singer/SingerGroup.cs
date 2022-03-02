using System;
using System.Collections.Generic;
using dotnetApp.dotnetApp.Dtos.Group;

namespace dotnetApp.dotnetApp.Dvos.Singer
{
  public class SingerGroups
  {
    public string id { get; set; }
    public string name { get; set; }
    public string avatar { get; set; }
    public string nickname { get; set; }
    public string gender { get; set; }
    public string biography { get; set; }
    public DateTime birth { get; set; }
    public string country { get; set; }
    public List<GroupRead> groups { get; set; }
  }
}
