using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnetApp.Dtos.Collection
{
  // 查詢收藏的歌曲
  [NotMapped]
  public class CollectionItem
  {
    public Guid id { get; set; }
    public string name { get; set; }
    public Guid? soundId { get; set; }
    public string soundName { get; set; }
  }
}
