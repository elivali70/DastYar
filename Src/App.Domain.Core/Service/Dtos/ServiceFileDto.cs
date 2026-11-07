using App.Domain.Core.Service.Entities;

namespace App.Domain.Core.Service.Dtos;

public class ServiceFileDto
{
    public int Id { get; set; }

    public int FileId { get; set; }

    public int ServiceId { get; set; }

    public DateTime CreatedAt { get; set; }
}

