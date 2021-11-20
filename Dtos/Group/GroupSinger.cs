using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnetApp.Dtos.Group
{
  [NotMapped]
  public class GroupSinger
  {
    public Guid id { get; set; }
    public string name { get; set; }
    public string singerId { get; set; }
    public string singerName { get; set; }
    public string singerAvatar { get; set; }
    public string singerNickname { get; set; }
    public string singerGender { get; set; }
    public string singerBirth { get; set; }
    public string singerCountry { get; set; }
  }
}
