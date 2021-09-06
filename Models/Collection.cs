using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnetApp.Models
{
  [Table("Collections")]
  public class Collection : Time
  {
    [Key]
    public Guid id { get; set; }
    [Required]
    [StringLength(30)]
    public string name { get; set; }
    [Required]
    [ForeignKey("FK_Member")]
    public Guid memberId { get; set; }

    #region 關聯
    public Member Member { get; set; }
    #endregion
  }
}
