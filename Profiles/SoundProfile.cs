using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using dotnetApp.Dtos.Collection;
using dotnetApp.Dtos.Sound;
using dotnetApp.Dvos.Collection;
using dotnetApp.Models;

namespace dotnetApp.Profiles
{
  public class SoundProfile : Profile
  {
    public SoundProfile()
    {
      CreateMap<Sound, SoundRead>();
      CreateMap<SoundCreate, Sound>();
      CreateMap<SoundUpdate, Sound>();
      CreateMap<Sound, SoundUpdate>();

      CreateMap<IList<CollectionSound>, CollectionSounds>()
      .ForMember(x => x.id, y => y.MapFrom(o => o.FirstOrDefault().id))
      .ForMember(x => x.name, y => y.MapFrom(o => o.FirstOrDefault().name))
      .ForMember(x => x.createdAt, y => y.MapFrom(o => o.FirstOrDefault().createdAt))
      .ForMember(x => x.sounds, y => y.MapFrom(o => o.Select(v => new SoundList { id = v.soundId, name = v.soundName, publishYear = v.soundPublishYear })));
    }
  }
}
