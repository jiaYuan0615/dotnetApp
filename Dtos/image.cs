using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace dotnetApp.Dtos
{
  public class ImageUpload
  {
    public IFormFile image { get; set; }
  }
}
