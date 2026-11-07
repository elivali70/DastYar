

namespace App.Domain.Core.Service.Entities;

public partial class ExpertFavoritService
{
    public int Id { get; set; }

    public int ExpertId { get; set; }

    public int ServiceId { get; set; }
    public bool IsDeleted {  get; set; }
    public DateTime CreatedAt { get; set; }
    public virtual Service Service { get; set; } = null!;

}
