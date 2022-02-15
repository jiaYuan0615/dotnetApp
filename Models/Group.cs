using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnetApp.Models
{
  [Table("groups")]
  public class Group : Time
  {
    [Key]
    public Guid id { get; set; }
    [Required]
    [StringLength(50)]
    public string name { get; set; }

    [Column(TypeName = "text")]
    public string biography { get; set; }
    [StringLength(7)]
    public string publishYear { get; set; }
    [Required]
    public string avatar { get; set; }

    #region m:n
    // public ICollection<Singer> Singers { get; set; }
    #endregion
  }
}
