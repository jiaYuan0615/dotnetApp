using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnetApp.dotnetApp.Models
{
  [Table("nlogs")]
  public class Nlog
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid id { get; set; } = Guid.NewGuid();
    [Required]
    [StringLength(10)]
    public string level { get; set; }
    [Required]
    [MaxLength]
    public string message { get; set; }
    [Required]
    [StringLength(255)]
    public string logger { get; set; }
    [MaxLength]
    public string exceptions { get; set; }
    [Required]
    [MaxLength]
    public string callsite { get; set; }
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime time { get; set; } = DateTime.Now;
  }
}
