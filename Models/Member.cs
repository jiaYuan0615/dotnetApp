using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using dotnetApp.Services;

namespace dotnetApp.Models
{
  [Table("Members")]
  public class Member : Time
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
    public string avatar { get; set; }
    [Required]
    [StringLength(20)]
    public string name { get; set; }
    [Required]
    [StringLength(1)]
    public string gender { get; set; }
    public DateTime email_verified { get; set; }

    #region 1:m
    public ICollection<Collection> Collections { get; set; }
    #endregion
  }
}
