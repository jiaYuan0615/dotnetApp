using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnetApp.Models
{
  [Table("Collections")]
  public class Collection
  {
    [Key]
    public Guid id { get; set; }
    [Required]
    [StringLength(30)]
    public string name { get; set; }
    [Required]
    [ForeignKey("memberId")]
    public Guid memberId { get; set; }
  }
}
