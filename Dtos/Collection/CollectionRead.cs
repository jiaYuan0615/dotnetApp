using System;

namespace dotnetApp.Dtos.Collection
{
  public class CollectionRead
  {
    public Guid id { get; set; }
    public string name { get; set; }
    public Guid memberId { get; set; }
    public DateTime createdAt { get; set; }
    public DateTime updatedAt { get; set; }
  }
}
