using AutoMapper;
using dotnetApp.Dtos;
using dotnetApp.Models;
using dotnetApp.Repositories.User;

namespace dotnetApp.Profiles
{
  public class MemberProfile : Profile
  {
    public MemberProfile()
    {
      // Propose
      // To inherit AutoMapper "Profile"

      // source -> target
      CreateMap<Member, MemberRead>();
      CreateMap<RegisterRepository, Member>();
    }
  }
}
