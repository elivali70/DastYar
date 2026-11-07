using App.Domain.Core.User.Entities;
using App.Domain.Core.Category.Entities;
using App.Domain.Core.Order.Enities;
using System;
using System.Collections.Generic;

namespace App.Domain.Core.Service.Entities;

public partial class Service
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int BasePrice { get; set; }

    public int CategoryId { get; set; }

    public string? Disctiption { get; set; }
    public bool IsDeleted { get; set; }

    public DateTime CreatedAt { get; set; }
    public virtual Category.Entities.Category Category { get; set; } = null!;

    public virtual ICollection<ExpertFavoritService> ExpertFavoritServices { get; set; } = new List<ExpertFavoritService>();

    public virtual ICollection<Expert> Experts { get; set; } = new List<Expert>();

    public virtual ICollection<Order.Enities.Order> Orders { get; set; } = new List<Order.Enities.Order>();

    public virtual ICollection<ServiceFile> ServiceFiles { get; set; } = new List<ServiceFile>();
}
