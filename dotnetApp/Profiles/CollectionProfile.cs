using System.Collections.Generic;
using AutoMapper;
using dotnetApp.dotnetApp.Dtos.Collection;
using dotnetApp.dotnetApp.Dvos.Collection;
using dotnetApp.dotnetApp.Models;
using System.Linq;
using dotnetApp.dotnetApp.Dvos.Sound;

namespace dotnetApp.dotnetApp.Profiles
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
      .ForMember(
        x => x.sounds,
        y => y.MapFrom(o => string.IsNullOrEmpty(o.FirstOrDefault().soundId.ToString()) ? new List<SoundItems>() : o.Select(v => new SoundItems { id = v.soundId.ToString(), name = v.soundName }).ToList())
      );
    }
  }
}
