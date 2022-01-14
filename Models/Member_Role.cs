using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnetApp.Models
{
  [Table("member_role")]
  public class Member_Role : Time
  {
    public Guid id { get; set; } = Guid.NewGuid();
    [ForeignKey("Member")]
    public Guid memberId { get; set; }
    [ForeignKey("Role")]
    public Guid roleId { get; set; }

    #region belongs
    public Member Member { get; set; }
    public Role Role { get; set; }
    #endregion
  }
}
