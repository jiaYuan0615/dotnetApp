using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnetApp.Models
{
  [Table("Profiles")]
  public class Profile
  {
    [Key]
    public Guid userId { get; set; }
    [Required]
    public string name { get; set; }
    [Required]
    public string phone { get; set; }
    [Required]
    public string address { get; set; }
  }
}