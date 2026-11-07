using App.Domain.Core.Order.Enities;

namespace App.Domain.Core.AppFile.Entities;

public class FileType
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Extention { get; set; }
    public bool IsDeleted { get; set; }
  
    public DateTime CreatedAt { get; set; }
    public virtual ICollection<AppFile> AppFiles { get; set; } = new List<AppFile>();

}
