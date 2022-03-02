using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnetApp.dotnetApp.Dtos.Collection
{
  // 查詢收藏的歌曲
  [NotMapped]
  public class CollectionSound
  {
    public Guid id { get; set; }
    public string name { get; set; }
    public Guid soundId { get; set; }
    public string soundName { get; set; }
    public string soundPublishYear { get; set; }
    public DateTime createdAt { get; set; }
  }
}
