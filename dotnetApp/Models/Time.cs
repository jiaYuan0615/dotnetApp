using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnetApp.dotnetApp.Models
{
  public abstract class Time
  {
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime createdAt { get; set; } = DateTime.Now;
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime updatedAt { get; set; }
  }
}
