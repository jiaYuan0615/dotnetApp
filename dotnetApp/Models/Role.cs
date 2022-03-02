
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnetApp.dotnetApp.Models
{
  [Table("roles")]
  public class Role : Time
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid id { get; set; } = Guid.NewGuid();
    [Required]
    [StringLength(20)]
    public string name { get; set; }
    public bool status { get; set; }
  }
}
