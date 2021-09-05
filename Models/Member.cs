using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using dotnetApp.Services;

namespace dotnetApp.Models
{
  [Table("Members")]
  public class Member
  {
    PasswordService passwordService = new PasswordService();
    private string hash;
    [Key]
    public Guid id { get; set; } = Guid.NewGuid();
    [Required]
    [EmailAddress]
    [StringLength(60)]
    public string email { get; set; }
    [Required]
    [StringLength(60)]
    public string password
    {
      get
      {
        return hash;
      }
      set
      {
        hash = passwordService.HashPassword(value);
      }
    }
    [Required]
    public string avatar { get; set; } = "avatar";
    [Required]
    [StringLength(20)]
    public string name { get; set; } = "chenyan";
    [Required]
    [StringLength(1)]
    public string gender { get; set; } = "1";
    [Required]
    public DateTime email_verified { get; set; } = DateTime.Now;
  }
}
