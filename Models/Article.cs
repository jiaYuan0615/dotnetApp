using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnetApp.Models
{
  [Table("Articles")]
  public class Article
  {
    [Key]
    public Guid id { get; set; }
    [Required]
    public string title { get; set; }
    [Required]
    public string description { get; set; }
    public DateTime publish { get; set; }
    [Required]
    public Guid UserId { get; set; }
  }
}