using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetApp.Context;
using dotnetApp.Dtos.Group;
using dotnetApp.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnetApp.Services
{
  public interface IGroupService
  {
    IEnumerable<Group> GetGroup();
    Group GetAssignGroup(Guid id);
    Task PostGroup(Group group);
    Task UpdateGroup(Group group, GroupUpdate groupUpdate);
    Task DeleteGroup(Group group);
  }
  public class GroupService : IGroupService
  {
    private readonly DatabaseContext _databaseContext;
    public GroupService(DatabaseContext databaseContext)
    {
      _databaseContext = databaseContext;

    }
    public async Task DeleteGroup(Group group)
    {
      _databaseContext.Remove(group);
      await _databaseContext.SaveChangesAsync();
    }

    public Group GetAssignGroup(Guid id)
    {
      return _databaseContext.Groups.Find(id);
    }

    public IEnumerable<Group> GetGroup()
    {
      return _databaseContext.Groups.AsNoTracking().ToList();
    }

    public async Task PostGroup(Group group)
    {
      _databaseContext.Groups.Add(group);
      await _databaseContext.SaveChangesAsync();
    }

    public async Task UpdateGroup(Group group, GroupUpdate groupUpdate)
    {
      _databaseContext.Entry(group).CurrentValues.SetValues(groupUpdate);
      await _databaseContext.SaveChangesAsync();
    }
  }
}
