using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using dotnetApp.dotnetApp.Dtos;

namespace dotnetApp.dotnetApp.Helpers
{
  public class CommonHelpers
  {
    // Equals to JavaScript Object.Keys method
    public static IEnumerable<string> objectKeys<T>(T data)
    {
      var item = typeof(T).GetProperties();
      IEnumerable<string> items = item.Select(x => x.Name);
      return items;
    }

    public static string JsonTranslateHandler(object param)
    {
      JsonSerializerOptions options = new JsonSerializerOptions
      {
        WriteIndented = true,
        Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
      };
      return JsonSerializer.Serialize(param, options);
    }

    // Equals to Lodash xor
    public static List<string> xor(List<string> origin, List<string> update)
    {
      // origin has but update doesn't have
      List<string> left = origin.Except(update).ToList();
      // update has but origin doesn't have
      List<string> right = update.Except(origin).ToList();
      return left.Concat(right).ToList();
    }


    public static UpdateItem updateItem(List<string> origin, List<string> update)
    {
      List<string> combine = xor(origin, update);
      List<string> needPreserve = origin.Intersect(update).ToList();
      List<string> needInsert = update.Except(needPreserve).ToList();
      List<string> needDelete = origin.Except(needPreserve).ToList();
      UpdateItem updateItem = new UpdateItem()
      {
        insertItem = needInsert,
        deleteItem = needDelete,
        shoudUpdate = combine.Count() > 0
      };
      return updateItem;
    }
  }
}
