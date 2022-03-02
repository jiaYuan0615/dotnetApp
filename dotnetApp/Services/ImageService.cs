using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetApp.dotnetApp.Context;
using dotnetApp.dotnetApp.Dtos.Member;
using dotnetApp.dotnetApp.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnetApp.dotnetApp.Services
{
  public class ImageService
  {
    private DatabaseContext _databaseContext;

    public ImageService(DatabaseContext databaseContext)
    {
      _databaseContext = databaseContext;
    }
    public Image GetAssignImageById(Guid id)
    {
      return _databaseContext.Images.Find(id);
    }

    public async Task PostImage(Image image)
    {
      _databaseContext.Images.Add(image);
      await _databaseContext.SaveChangesAsync();
    }

    public async Task DelteImage(Image image)
    {
      _databaseContext.Images.Remove(image);
      await _databaseContext.SaveChangesAsync();
    }
  }
}
