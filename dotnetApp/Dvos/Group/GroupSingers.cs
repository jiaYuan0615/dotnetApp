using System;
using System.Collections.Generic;
using dotnetApp.dotnetApp.Dtos.Singer;

namespace dotnetApp.dotnetApp.Dvos.Group
{
  public class GroupSingers
  {
    public Guid id { get; set; }
    public string name { get; set; }
    public List<SingerRead> singers { get; set; }
  }
}
