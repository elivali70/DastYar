using App.Domain.Core.AppFile.Dtos;
using App.Domain.Core.Service.Entities;

namespace App.Domain.Core.Service.Dtos;

public class ExpertFavoritServiceDto
{
    public int Id { get; set; }

    public int ExpertId { get; set; }

    public int ServiceId { get; set; }
    public DateTime CreatedAt { get; set; }
}
