using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace dotnetApp.Models
{
  [Table("images")]
  public class Image
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid id { get; set; } = Guid.NewGuid();

    [Required]
    public string fileName { get; set; }
    [Required]
    public int length { get; set; }
    [Required]
    public string contentType { get; set; }
    [Required]
    public string path { get; set; }
  }
}
