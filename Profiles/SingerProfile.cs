using System.Collections.Generic;
using AutoMapper;
using dotnetApp.Dtos.Singer;
using dotnetApp.Models;

namespace dotnetApp.Profiles
{
  public class SingerProfile : Profile
  {
    public SingerProfile()
    {
      CreateMap<Singer, SingerRead>();
      CreateMap<SingerCreate, Singer>();
      CreateMap<SingerUpdate, Singer>();
      CreateMap<Singer, SingerUpdate>();

      CreateMap<IList<SingerSound>, SingerSounds>();
    }
  }
}
