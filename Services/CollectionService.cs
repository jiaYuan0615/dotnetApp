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
    List<Collection> GetCollection(Guid id);
    Collection GetAssignCollectionById(Guid id);
    List<CollectionSound> GetCollectionSound(string id, string memberId);
    Task<string> PostCollection(Collection collection);
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

    public List<Collection> GetCollection(Guid id)
    {
      var collection = _databaseContext.Collections
      .AsNoTracking()
      .Where(x => x.memberId == id)
      .ToList();
      return collection;
    }

    public List<CollectionSound> GetCollectionSound(string id, string memberId)
    {
      FormattableString sql = $@"
      SELECT
        collections.id,
        collections.`name`,
        `sounds`.id AS soundId,
        `sounds`.`name` AS soundName,
        `sounds`.publishYear AS soundPublishYear
        collections.createdAt
      FROM
        collections
        LEFT JOIN ( collection_sound AS cs INNER JOIN `sounds` ON `sounds`.id = cs.soundId ) ON cs.collectionId = collections.id
      WHERE
        collections.memberId = {memberId}
        AND collections.id = {id}";
      List<CollectionSound> collectionSound = _databaseContext.CollectionSounds
      .FromSqlInterpolated(sql)
      .AsNoTracking()
      .ToList();

      return collectionSound;
    }

    public async Task<string> PostCollection(Collection collection)
    {
      _databaseContext.Add(collection);
      await _databaseContext.SaveChangesAsync();
      return collection.id.ToString();
    }

    public async Task UpdateCollection(Collection collection, CollectionUpdate collectionUpdate)
    {
      _databaseContext.Entry(collection).CurrentValues.SetValues(collectionUpdate);
      await _databaseContext.SaveChangesAsync();
    }
  }
}
