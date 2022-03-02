
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnetApp.dotnetApp.Models
{
  [Table("permissions")]
  public class Permission : Time
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid id { get; set; } = Guid.NewGuid();
    [Required]
    [StringLength(20)]
    public string title { get; set; }
    [Required]
    [StringLength(100)]
    public string action { get; set; }
    public bool status { get; set; }
  }
}
