using AutoMapper;
using dotnetApp.Dtos.Member;
using dotnetApp.Models;

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
      CreateMap<MemberRegister, Member>();
      CreateMap<MemberUpdate, Member>();
      CreateMap<Member, MemberUpdate>();
    }
  }
}
