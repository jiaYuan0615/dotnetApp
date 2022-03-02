using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnetApp.dotnetApp.Models
{
  [Table("images")]
  public class Image
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid id { get; set; } = Guid.NewGuid();

    [Required]
    public string FileName { get; set; }
    [Required]
    public int Length { get; set; }
    [Required]
    public string ContentType { get; set; }
    [Required]
    public string path { get; set; }
  }
}
