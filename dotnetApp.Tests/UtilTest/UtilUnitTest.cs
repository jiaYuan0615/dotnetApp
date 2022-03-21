using System.Collections.Generic;
using System.Text.Json;
using dotnetApp.dotnetApp.Dtos;
using dotnetApp.dotnetApp.Helpers;
using NUnit.Framework;

namespace dotnetApp.Tests.UnitTest
{
  public class UtilUnitTest
  {
    [SetUp]
    public void SetUp()
    {

    }

    [Test]
    public void TestUpdateItem()
    {
      // 測試物件相等需要轉換成 JSON 字串比對才會過測試
      // 測試陣列比對則不需要
      List<string> origin = new List<string>() { "1", "2", "3" };
      List<string> update = new List<string>() { "2", "3", "4" };
      // Arrange
      UpdateItem updateItem = new UpdateItem()
      {
        insertItem = new List<string>() { "4" },
        deleteItem = new List<string>() { "1" },
        shoudUpdate = true,
      };
      string expect = CommonHelpers.JsonTranslateHandler(updateItem);
      // Act
      UpdateItem param = CommonHelpers.updateItem(origin, update);
      string actual = CommonHelpers.JsonTranslateHandler(param);
      // Assert
      Assert.AreEqual(expect, actual);
    }
  }
}