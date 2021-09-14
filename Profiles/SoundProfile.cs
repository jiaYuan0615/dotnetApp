using AutoMapper;
using dotnetApp.Dtos.Sound;
using dotnetApp.Models;

namespace dotnetApp.Profiles
{
  public class SoundProfile : Profile
  {
    public SoundProfile()
    {
      CreateMap<Sound, SoundRead>();
      CreateMap<SoundCreate, Sound>();
      CreateMap<SoundUpdate, Sound>();
      CreateMap<Sound, SoundUpdate>();
    }
  }
}
