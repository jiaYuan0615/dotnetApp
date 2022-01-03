using AutoMapper;
using dotnetApp.Dtos;
using dotnetApp.Models;

namespace dotnetApp.Profiles
{
  public class ImageProfile : Profile
  {
    public ImageProfile()
    {
      CreateMap<ImageUpload, Image>();
    }
  }
}
