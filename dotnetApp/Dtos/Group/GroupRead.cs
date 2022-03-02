using System;

namespace dotnetApp.dotnetApp.Dtos.Group
{
  public class GroupRead
  {
    public string id { get; set; }
    public string name { get; set; }
    public string biography { get; set; }
    public string publishYear { get; set; }
    public string avatar { get; set; }
    public DateTime createdAt { get; set; }
    public DateTime updatedAt { get; set; }
  }
}
