using AutoMapper;
using dotnetApp.dotnetApp.Dtos;
using dotnetApp.dotnetApp.Models;
using Microsoft.AspNetCore.Http;

namespace dotnetApp.dotnetApp.Profiles
{
  public class ImageProfile : Profile
  {
    public ImageProfile()
    {
      CreateMap<ImageUpload, Image>();
    }
  }
}
