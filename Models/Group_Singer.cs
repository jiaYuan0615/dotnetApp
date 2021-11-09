using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnetApp.Models
{
  [Table("Group_Singer")]
  public class Group_Singer : Time
  {
    public Guid id { get; set; } = Guid.NewGuid();
    [ForeignKey("Group")]
    public Guid groupId { get; set; }
    [ForeignKey("Singer")]
    public Guid singerId { get; set; }

    #region belongs
    public Group Group { get; set; }
    public Singer Singer { get; set; }
    #endregion
  }
}
