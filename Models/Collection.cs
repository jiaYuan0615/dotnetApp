using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnetApp.Models
{
  [Table("collections")]
  public class Collection : Time
  {
    [Key]
    public Guid id { get; set; } = Guid.NewGuid();
    [Required]
    [StringLength(30)]
    public string name { get; set; }
    [Required]
    [ForeignKey("Member")]
    public Guid memberId { get; set; }

    #region belongs
    public Member Member { get; set; }
    #endregion
    #region m:n
    // public ICollection<Sound> Sounds { get; set; }
    #endregion
  }
}
