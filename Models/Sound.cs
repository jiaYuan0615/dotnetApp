using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnetApp.Models
{
  [Table("Sounds")]
  public class Sound : Time
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid id { get; set; } = Guid.NewGuid();
    [Required]
    [StringLength(60)]
    public string name { get; set; }
    public string lyrics { get; set; }
    [Required]
    [StringLength(4)]
    public string publishYear { get; set; }
    [Required]
    public string cover { get; set; }
    [Required]
    [StringLength(30)]
    public string OST { get; set; }
    [Required]
    public bool isCover { get; set; }

    #region m:n
    // public ICollection<Singer> Singers { get; set; }
    // public ICollection<Collection> Collections { get; set; }
    #endregion
  }
}
