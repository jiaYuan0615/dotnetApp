using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnetApp.Models
{
  [Table("members")]
  public class Member : Time
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid id { get; set; } = Guid.NewGuid();
    [Required]
    [EmailAddress]
    [StringLength(60)]
    public string email { get; set; }
    [Required]
    [StringLength(60)]
    public string password { get; set; }
    public string avatar { get; set; }
    [Required]
    [StringLength(20)]
    public string name { get; set; }
    [Required]
    [StringLength(1)]
    public string gender { get; set; }
    public DateTime? email_verified { get; set; }

    #region 1:m
    public ICollection<Collection> Collections { get; set; }
    #endregion
  }
}
