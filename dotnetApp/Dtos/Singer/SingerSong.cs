using System.ComponentModel.DataAnnotations.Schema;

namespace dotnetApp.dotnetApp.Dtos.Singer
{
  // 查詢歌手演唱過的歌曲
  [NotMapped]
  public class SingerSound
  {
    public string id { get; set; }
    public string name { get; set; }
    public string nickName { get; set; }
    public string gender { get; set; }
    public string avatar { get; set; }
    public string birth { get; set; }
    public string country { get; set; }
    public string groupId { get; set; }
    public string groupName { get; set; }
    public string soundId { get; set; }
    public string soundName { get; set; }
    public string soundPublishYear { get; set; }
  }
}
