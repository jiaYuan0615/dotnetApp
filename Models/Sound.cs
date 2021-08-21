using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnetApp.Models
{
  [Table("Sounds")]
  public class Sound
  {
    [Key]
    public Guid id { get; set; }
    [Required]
    public string name { get; set; }
    public string lyrics { get; set; }
    [Required]
    public string publishYear { get; set; }
    [Required]
    public string cover { get; set; }
    [Required]
    public string OST { get; set; }
    [Required]
    public bool isCover { get; set; }
  }
}