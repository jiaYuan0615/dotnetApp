using System.Collections.Generic;

namespace dotnetApp.dotnetApp.Dtos
{
  public class UpdateItem
  {
    public List<string> insertItem { get; set; }
    public List<string> deleteItem { get; set; }
    public bool shoudUpdate { get; set; }
  }
}
