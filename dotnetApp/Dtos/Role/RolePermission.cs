using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace dotnetApp.dotnetApp.Dtos.Role
{
  [NotMapped]
  public class RolePermission
  {
    public Guid id { get; set; }
    public string name { get; set; }
    public bool status { get; set; }
    public Guid permissionId { get; set; }
    public string action { get; set; }
  }
}
