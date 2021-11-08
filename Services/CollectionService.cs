using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetApp.Context;
using Microsoft.EntityFrameworkCore;
using dotnetApp.Dtos.Collection;
using dotnetApp.Models;

namespace dotnetApp.Services
{
  public interface ICollectionService
  {
    IEnumerable<Collection> GetCollection(Guid id);
    Collection GetAssignCollectionById(Guid id);
    Task PostCollection(Collection collection);
    Task UpdateCollection(Collection collection, CollectionUpdate collectionUpdate);
    Task DeleteCollection(Collection collection);

  }
  public class CollectionService : ICollectionService
  {
    private readonly DatabaseContext _databaseContext;

    public CollectionService(DatabaseContext databaseContext)
    {
      _databaseContext = databaseContext;
    }

    public async Task DeleteCollection(Collection collection)
    {
      _databaseContext.Remove(collection);
      await _databaseContext.SaveChangesAsync();
    }

    public Collection GetAssignCollectionById(Guid id)
    {
      return _databaseContext.Collections.Find(id);
    }

    public IEnumerable<Collection> GetCollection(Guid id)
    {
      var collection = _databaseContext.Collections
      .AsNoTracking()
      .Where(x => x.memberId == id)
      .ToList();
      return collection;
    }

    public async Task PostCollection(Collection collection)
    {
      _databaseContext.Add(collection);
      await _databaseContext.SaveChangesAsync();
    }

    public async Task UpdateCollection(Collection collection, CollectionUpdate collectionUpdate)
    {
      _databaseContext.Entry(collection).CurrentValues.SetValues(collectionUpdate);
      await _databaseContext.SaveChangesAsync();
    }
  }
}
