using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnetApp.Models
{
  [Table("Singer_Sound")]
  public class Singer_Sound : Time
  {
    public Guid id { get; set; } = Guid.NewGuid();
    [ForeignKey("Singer")]
    public Guid singerId { get; set; }
    [ForeignKey("Sound")]
    public Guid soundId { get; set; }

    #region belongs
    public Singer Singer { get; set; }
    public Sound Sound { get; set; }
    #endregion
  }
}
