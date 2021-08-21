using System;
using System.Collections.Generic;
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
    public string email { get; set; }
    [Required]
    public string password { get; set; }
    [Required]
    public string avatar { get; set; }
    [Required]
    public string name { get; set; }
    [Required]
    public string gender { get; set; }
    [Required]
    public  DateTime email_verified { get; set; }
  }
}