using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnetApp.Dtos.Member
{
  // 取得 SQL 查詢結果的 model
  [NotMapped]
  public class MemberCollection
  {
    public Guid id { get; set; }
    public string email { get; set; }
    public string name { get; set; }
    public string gender { get; set; }
    public Guid collectionId { get; set; }
    public string collectionName { get; set; }
  }
}
