using System;

namespace dotnetApp.Dtos.Group
{
  public class GroupRead
  {
    public Guid id { get; set; }
    public string name { get; set; }
    public DateTime createdAt { get; set; }
    public DateTime updatedAt { get; set; }
  }
}
