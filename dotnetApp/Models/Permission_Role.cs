using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnetApp.dotnetApp.Models
{
  [Table("permission_role")]
  public class Permission_Role : Time
  {
    public Guid id { get; set; } = Guid.NewGuid();
    [ForeignKey("Permission")]
    public Guid permissionId { get; set; }
    [ForeignKey("Role")]
    public Guid roleId { get; set; }

    #region belongs
    public Permission Permission { get; set; }
    public Role Role { get; set; }
    #endregion
  }
}
