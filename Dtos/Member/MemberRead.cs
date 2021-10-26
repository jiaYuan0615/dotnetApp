using System;
using dotnetApp.Dtos.Collection;

namespace dotnetApp.Dtos.Member
{
  public class MemberRead
  {
    public Guid Id { get; set; }
    public string email { get; set; }
    public string avatar { get; set; }
    public string name { get; set; }
    public string gender { get; set; }
  }
}
