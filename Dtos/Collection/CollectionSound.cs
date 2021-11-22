using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnetApp.Dtos.Collection
{
  // 查詢收藏的歌曲
  [NotMapped]
  public class CollectionSound
  {
    public string id { get; set; }
    public string name { get; set; }
    public string soundId { get; set; }
    public string soundName { get; set; }
    public string soundPublishYear { get; set; }
    public string createdAt { get; set; }
  }
}
