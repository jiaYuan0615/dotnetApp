using System.Collections.Generic;
using System.Threading.Tasks;
using dotnetApp.Context;
using dotnetApp.Models;
using dotnetApp.Repositories.User;

namespace dotnetApp.Services
{
  public class UserService
  {
    private readonly DatabaseContext _DatabaseContext;
    public UserService(DatabaseContext DatabaseContext)
    {
      _DatabaseContext = DatabaseContext;
    }

    public async Task<bool> RegisterUser(RegisterRepository registerRepository)
    {
      try
      {
        User user = new User();
        await _DatabaseContext.AddAsync(registerRepository);
        await _DatabaseContext.SaveChangesAsync();
        return true;
      }
      catch (System.Exception)
      {
        return false;
      }

    }
  }
}