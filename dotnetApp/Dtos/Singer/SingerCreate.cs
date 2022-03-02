using System;
using Microsoft.AspNetCore.Http;

namespace dotnetApp.dotnetApp.Dtos.Singer
{
  public class SingerCreate
  {
    public string name { get; set; }
    public IFormFile avatar { get; set; }
    public string biography { get; set; }
    public Guid groupId { get; set; }
    public string nickname { get; set; }
    public string gender { get; set; }
    public DateTime birth { get; set; }
    public string country { get; set; }
  }
}
