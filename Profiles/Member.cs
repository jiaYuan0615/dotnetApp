using AutoMapper;
using dotnetApp.Dtos;
using dotnetApp.Models;

namespace dotnetApp.Profiles
{
  public class MemberProfile : Profile
  {
    public MemberProfile()
    {
      // source -> target
      CreateMap<Member, MemberRead>();
    }
  }
}
