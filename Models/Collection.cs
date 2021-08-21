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
    public string name { get; set; }
    [Required]
    public Guid memberId { get; set; }
  }
}