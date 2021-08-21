using System.Collections.Generic;
using System.Threading.Tasks;
using dotnetApp.Context;
using dotnetApp.Models;
using dotnetApp.Repositories.User;

namespace dotnetApp.Services
{
  public class MemberService
  {
    private readonly DatabaseContext _DatabaseContext;
    public MemberService(DatabaseContext DatabaseContext)
    {
      _DatabaseContext = DatabaseContext;
    }

    public async Task<bool> RegisterMember(RegisterRepository registerRepository)
    {
      try
      {
        _DatabaseContext.Add(registerRepository);
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