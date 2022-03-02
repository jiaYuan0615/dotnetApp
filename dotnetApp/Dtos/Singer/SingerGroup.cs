using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnetApp.dotnetApp.Dtos.Singer
{
  [NotMapped]
  public class SingerGroup
  {
    public Guid id { get; set; }
    public string name { get; set; }
    public string avatar { get; set; }
    public string nickname { get; set; }
    public string gender { get; set; }
    public string biography { get; set; }
    public DateTime birth { get; set; }
    public string country { get; set; }
    public Guid groupId { get; set; }
    public string groupName { get; set; }
    public DateTime groupCreatedAt { get; set; }
    public DateTime groupupdatedAt { get; set; }
  }
}
