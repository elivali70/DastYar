using App.Domain.Core.Order.Dtos;
using System;
using System.Collections.Generic;

namespace App.Domain.Core.Order.Enities;

public class CommentDto
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public string? Discription { get; set; }

    public DateTime CreatedAt { get; set; }
    public int UserId { get; set; }

}
