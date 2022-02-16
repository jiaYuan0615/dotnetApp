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
  public class CollectionService
  {
    private readonly DatabaseContext _databaseContext;

    public CollectionService(DatabaseContext databaseContext)
    {
      _databaseContext = databaseContext;
    }

    public async Task DeleteCollection(Guid id)
    {
      Collection collections = _databaseContext.Collections
                        .Where(x => x.id == id)
                        .Include(x => x.Member)
                        .FirstOrDefault();
      _databaseContext.Remove(collections);
      await _databaseContext.SaveChangesAsync();
    }

    public Collection GetAssignCollectionById(Guid id)
    {
      return _databaseContext.Collections.Find(id);
    }

    public List<Collection> GetCollection(Guid id)
    {
      List<Collection> collection = _databaseContext.Collections
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
      _databaseContext.Collections.Add(collection);
      await _databaseContext.SaveChangesAsync();
      return collection.id.ToString();
    }

    public async Task PostItemToCollection(Collection_Sound collection_Sound)
    {
      _databaseContext.Collection_Sound.Add(collection_Sound);
      await _databaseContext.SaveChangesAsync();
    }

    public async Task PostMultiItemToCollection(List<Collection_Sound> collection_Sounds)
    {
      await _databaseContext.Collection_Sound.AddRangeAsync(collection_Sounds);
      await _databaseContext.SaveChangesAsync();
    }

    public async Task UpdateCollection(Collection collection, CollectionUpdate collectionUpdate)
    {
      _databaseContext.Entry(collection).CurrentValues.SetValues(collectionUpdate);
      await _databaseContext.SaveChangesAsync();
    }

    public async Task DeleteMultiItemToCollection(List<Collection_Sound> collection_Sounds)
    {
      _databaseContext.Collection_Sound.RemoveRange(collection_Sounds);
      await _databaseContext.SaveChangesAsync();
    }

    public async Task UpdateCollection()
    {
      await _databaseContext.SaveChangesAsync();
    }
  }
}
