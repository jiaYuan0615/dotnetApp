using System;
using System.Globalization;

namespace dotnetApp.dotnetApp.Helpers
{
  public class NotFoundException : Exception
  {
    public NotFoundException()
    {
    }
    public NotFoundException(string message) : base(message) { }

    public NotFoundException(string message, params object[] args)
        : base(String.Format(CultureInfo.CurrentCulture, message, args))
    {
    }
  }
}
