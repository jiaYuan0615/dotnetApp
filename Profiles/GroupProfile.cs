using AutoMapper;
using dotnetApp.Dtos.Group;
using dotnetApp.Models;

namespace dotnetApp.Profiles
{
  public class GroupProfile : Profile
  {
    public GroupProfile()
    {
      CreateMap<Group, GroupRead>();
      CreateMap<GroupCreate, Group>();
      CreateMap<GroupUpdate, Group>();
      CreateMap<Group, GroupUpdate>();
    }
  }
}
