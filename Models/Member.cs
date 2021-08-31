using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnetApp.Models
{
  [Table("Members")]
  public class Member
  {
    [Key]
    public Guid id { get; set; }
    [Required]
    [EmailAddress]
    [StringLength(60)]
    public string email { get; set; }
    [Required]
    [StringLength(60)]
    public string password { get; set; }
    [Required]
    public string avatar { get; set; }
    [Required]
    [StringLength(20)]
    public string name { get; set; }
    [Required]
    [StringLength(1)]
    public string gender { get; set; }
    [Required]
    public DateTime email_verified { get; set; }
  }
}
