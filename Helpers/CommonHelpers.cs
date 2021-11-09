using System;
using System.Collections.Generic;
using System.Linq;

namespace dotnetApp.Helpers
{
  public class CommonHelpers
  {
    // 判斷是否有需要更新關聯表的內容
    public static IEnumerable<T> xor<T>(List<T> origin, List<T> update)
    {
      // origin has but update doesn't have
      var first = origin.Except(update);
      // update has but origin doesn't have
      var second = update.Except(origin);
      return first.Concat(second);
    }
  }
}
