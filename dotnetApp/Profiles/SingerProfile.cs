using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using dotnetApp.dotnetApp.Dtos.Group;
using dotnetApp.dotnetApp.Dtos.Singer;
using dotnetApp.dotnetApp.Dvos.Singer;
using dotnetApp.dotnetApp.Models;

namespace dotnetApp.dotnetApp.Profiles
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

      CreateMap<IList<SingerGroup>, SingerGroups>()
      .ForMember(x => x.id, y => y.MapFrom(o => o.FirstOrDefault().id.ToString()))
      .ForMember(x => x.name, y => y.MapFrom(o => o.FirstOrDefault().name))
      .ForMember(x => x.avatar, y => y.MapFrom(o => o.FirstOrDefault().avatar))
      .ForMember(x => x.biography, y => y.MapFrom(o => o.FirstOrDefault().biography))
      .ForMember(x => x.nickname, y => y.MapFrom(o => o.FirstOrDefault().nickname))
      .ForMember(x => x.gender, y => y.MapFrom(o => o.FirstOrDefault().gender))
      .ForMember(x => x.birth, y => y.MapFrom(o => o.FirstOrDefault().birth))
      .ForMember(x => x.country, y => y.MapFrom(o => o.FirstOrDefault().country))
      .ForMember(x => x.groups, y => y.MapFrom(o => o.Select(x =>
      new GroupRead
      {
        id = x.groupId.ToString(),
        name = x.groupName,
        createdAt = x.groupCreatedAt,
        updatedAt = x.groupupdatedAt
      })));
    }
  }
}
