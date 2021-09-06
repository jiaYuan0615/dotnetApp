using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnetApp.Models
{
  [Table("Groups")]
  public class Group : Time
  {
    [Key]
    public Guid id { get; set; }
    [Required]
    [StringLength(50)]
    public string name { get; set; }
  }
}
