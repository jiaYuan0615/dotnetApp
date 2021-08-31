using dotnetApp.Context;

namespace dotnetApp.Services
{
  public class SoundService
  {
    private readonly DatabaseContext _DatabaseContext;
    public SoundService(DatabaseContext DatabaseContext)
    {
        _DatabaseContext = DatabaseContext;
    }
  }
}
