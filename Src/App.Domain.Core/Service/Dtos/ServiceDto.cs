namespace App.Domain.Core.Service.Dtos;

public class ServiceDto
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int BasePrice { get; set; }

    public int CategoryId { get; set; }

    public string? Disctiption { get; set; }
    public DateTime CreatedAt { get; set; }

}
