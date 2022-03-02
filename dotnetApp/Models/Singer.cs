using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnetApp.dotnetApp.Models
{
  [Table("singers")]
  public class Singer : Time
  {
    [Key]
    public Guid id { get; set; }
    [Required]
    [StringLength(60)]
    public string name { get; set; }
    [Required]
    public string avatar { get; set; }
    [Required]
    public string biography { get; set; }
    [ForeignKey("Group")]
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
    #region belongs
    public Group Group { get; set; }
    #endregion

    #region m:n
    // public ICollection<Sound> Sounds { get; set; }
    #endregion
  }
}
