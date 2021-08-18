using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnetApp.Models
{
  [Table("Users")]
  public class User
  {
    [Key]
    public Guid id { get; set; }
    [Required]
    [EmailAddress]
    public string email { get; set; }
    [Required]
    public string password { get; set; }

    public ICollection<Article> Articles { get; set; }
  }
}