using System;

namespace dotnetApp.Models
{
  public abstract class Time
  {
    public DateTime createdAt { get; set; } = DateTime.Now;
    public DateTime updatedAt { get; set; } = DateTime.Now;
  }
}
