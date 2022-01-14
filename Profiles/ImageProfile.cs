using AutoMapper;
using dotnetApp.Dtos;
using dotnetApp.Models;
using Microsoft.AspNetCore.Http;

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
