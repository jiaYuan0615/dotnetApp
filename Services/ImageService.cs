using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetApp.Context;
using dotnetApp.Dtos.Member;
using dotnetApp.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnetApp.Services
{

  public interface IImageService
  {
    Image GetAssignImageById(Guid id);
    Task PostImage(Image image);
  }
  public class ImageService : IImageService
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
  }
}
