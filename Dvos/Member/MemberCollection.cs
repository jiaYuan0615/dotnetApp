using System;

namespace dotnetApp.Dvos.Member
{
  // 取得 SQL 查詢結果的 model
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