using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnetApp.Models
{
  [Table("Collection_Sound")]
  public class Collection_Sound : Time
  {
    public Guid id { get; set; } = Guid.NewGuid();
    [ForeignKey("Collection")]
    public Guid collectionId { get; set; }
    [ForeignKey("Sound")]
    public Guid soundId { get; set; }

    #region  belongs
    public Collection Collection { get; set; }
    public Sound Sound { get; set; }
    #endregion
  }
}
