using System;
using System.Collections.Generic;

namespace App.Domain.Core.Order.Enities;

public partial class Comment
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public string? Discription { get; set; }

    public DateTime CreatedAt { get; set; }

    public int UserId { get; set; }

    public bool IsDeleted { get; set; }

    public virtual Order Order { get; set; } = null!;
}
