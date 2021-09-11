using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnetApp.Models
{
  [Table("Singers")]
  public class Singer : Time
  {
    [Key]
    public Guid id { get; set; }
    [Required]
    [StringLength(60)]
    public string name { get; set; }
    [Required]
    public string avatar { get; set; }
    [StringLength(300)]
    public string biography { get; set; }
    [Required]
    [ForeignKey("FK_Group")]
    public Guid groupId { get; set; }
    [Required]
    [StringLength(60)]
    public string nickname { get; set; }
    [Required]
    public string gender { get; set; }
    [Required]
    public DateTime birth { get; set; }
    [Required]
    [StringLength(20)]
    public string country { get; set; }

    #region m:n
    // public ICollection<Sound> Sounds { get; set; }
    // public ICollection<Group> Groups { get; set; }
    #endregion
  }
}
