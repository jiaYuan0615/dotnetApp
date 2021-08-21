using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnetApp.Models
{
  [Table("Groups")]
  public class Group
  {
    [Key]
    public Guid id { get; set; }
    [Required]
    public string name { get; set; }
  }
}