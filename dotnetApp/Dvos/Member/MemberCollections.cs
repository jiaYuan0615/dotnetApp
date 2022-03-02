using System.Collections.Generic;
using dotnetApp.dotnetApp.Dtos.Collection;

namespace dotnetApp.dotnetApp.Dvos.Member
{
  // 輸出結果的 model
  public class MemberCollections
  {
    public string id { get; set; }
    public string email { get; set; }
    public string name { get; set; }
    public string gender { get; set; }
    public List<CollectionRead> collections { get; set; }
  }
}
