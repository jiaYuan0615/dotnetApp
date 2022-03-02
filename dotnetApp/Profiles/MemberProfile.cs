using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using dotnetApp.dotnetApp.Dtos.Collection;
using dotnetApp.dotnetApp.Dtos.Member;
using dotnetApp.dotnetApp.Dvos.Member;
using dotnetApp.dotnetApp.Models;

namespace dotnetApp.dotnetApp.Profiles
{
  public class MemberProfile : Profile
  {
    public MemberProfile()
    {
      // Propose
      // To inherit AutoMapper "Profile"

      // source -> target
      CreateMap<Member, MemberRead>();
      CreateMap<MemberRegister, Member>();
      CreateMap<MemberUpdate, Member>();
      CreateMap<Member, MemberUpdate>();
      CreateMap<MemberUpdatePassword, Member>();
      CreateMap<Member, MemberUpdatePassword>();

      CreateMap<IList<MemberRole>, MemberRoles>()
      .ForMember(x => x.id, y => y.MapFrom(o => o.FirstOrDefault().id))
      .ForMember(x => x.Roles, y => y.MapFrom(o => o.Select(v => v.name).ToList()));

      // 1:m
      CreateMap<IList<MemberCollection>, MemberCollections>()
      .ForMember(x => x.id, y => y.MapFrom(o => o.FirstOrDefault().id.ToString()))
      .ForMember(x => x.email, y => y.MapFrom(o => o.FirstOrDefault().email))
      .ForMember(x => x.name, y => y.MapFrom(o => o.FirstOrDefault().name))
      .ForMember(x => x.gender, y => y.MapFrom(o => o.FirstOrDefault().gender))
      .ForMember(x => x.collections, y => y.MapFrom(o => o.Select(v => new CollectionRead { id = v.collectionId.ToString(), name = v.collectionName })));
    }
  }
}
