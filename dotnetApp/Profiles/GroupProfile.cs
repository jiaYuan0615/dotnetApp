using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using dotnetApp.dotnetApp.Dtos.Group;
using dotnetApp.dotnetApp.Dvos.Group;
using dotnetApp.dotnetApp.Models;
using dotnetApp.dotnetApp.Dtos.Singer;
using System;

namespace dotnetApp.dotnetApp.Profiles
{
  public class GroupProfile : Profile
  {
    public GroupProfile()
    {
      CreateMap<Group, GroupRead>();
      CreateMap<GroupCreate, Group>();
      CreateMap<GroupUpdate, Group>();
      CreateMap<Group, GroupUpdate>();

      CreateMap<IList<GroupSinger>, GroupSingers>()
      .ForMember(x => x.id, y => y.MapFrom(o => o.FirstOrDefault().id))
      .ForMember(x => x.name, y => y.MapFrom(o => o.FirstOrDefault().name))
      .ForMember(x => x.singers, y => y.MapFrom(o => o.Select(x =>
      new SingerRead
      {
        id = x.singerId,
        name = x.singerName,
        avatar = x.singerAvatar,
        nickname = x.singerNickname,
        gender = x.singerGender,
        birth = DateTime.Parse(x.singerBirth),
        country = x.singerBirth,
      })));

    }
  }
}
