namespace dotnetApp.Utils
{
  public class LogEvent
  {
    public const int process = 1000;
    public const int create = 1001;
    public const int read = 1002;
    public const int update = 1003;
    public const int delete = 1004;
    public const int success = 1005;
    public const int error = 1006;
    public const int NotFound = 404;
    public const int BadRequest = 400;
    public const int Unauthorized = 401;
    public const int Forbidden = 403;
  }
}
