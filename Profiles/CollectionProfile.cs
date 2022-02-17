using System.Collections.Generic;
using AutoMapper;
using dotnetApp.Dtos.Collection;
using dotnetApp.Dvos.Collection;
using dotnetApp.Models;
using System.Linq;

namespace dotnetApp.Profiles
{
  public class CollectionProfile : Profile
  {
    public CollectionProfile()
    {
      CreateMap<Collection, CollectionRead>();
      CreateMap<CollectionCreate, Collection>();
      CreateMap<CollectionUpdate, Collection>();
      CreateMap<Collection, CollectionUpdate>();


      CreateMap<IList<CollectionItem>, CollectionItems>()
      .ForMember(x => x.id, y => y.MapFrom(o => o.FirstOrDefault().id))
      .ForMember(x => x.name, y => y.MapFrom(o => o.FirstOrDefault().name))
      .ForMember(x => x.sounds, y => y.MapFrom(o => o.Select(v => v.soundId).ToList()));
    }
  }
}
