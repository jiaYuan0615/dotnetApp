using AutoMapper;
using dotnetApp.Dtos.Collection;
using dotnetApp.Models;

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
    }
  }
}
