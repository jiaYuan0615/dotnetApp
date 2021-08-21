using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnetApp.Models
{
  [Table("Singers")]
  public class Singer
  {
    [Key]
    public Guid id { get; set; }
    [Required]
    public string name { get; set; }
    [Required]
    public string avatar { get; set; }
    public string biography { get; set; }
    [Required]
    public Guid groupId { get; set; }
    [Required]
    public string nickname { get; set; }
    [Required]
    public string gender { get; set; }
    [Required]
    public string birth { get; set; }
    [Required]
    public string country { get; set; }
  }
}