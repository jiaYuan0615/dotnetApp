using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnetApp.dotnetApp.Models
{
  [Table("group_sound")]
  public class Group_Sound : Time
  {
    public Guid id { get; set; } = Guid.NewGuid();
    [ForeignKey("Group")]
    public Guid groupId { get; set; }
    [ForeignKey("Sound")]
    public Guid soundId { get; set; }

    #region belongs
    public Group Group { get; set; }
    public Sound Sound { get; set; }
    #endregion
  }
}
