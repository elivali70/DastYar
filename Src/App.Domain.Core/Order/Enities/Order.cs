using App.Domain.Core.Order.Enums;
using App.Domain.Core.User.Entities;
using System;
using System.Collections.Generic;

namespace App.Domain.Core.Order.Enities;

public partial class Order
{
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public int ServiceId { get; set; }

    public DateTime CreatedAt { get; set; }

    public OrderStatusEnum OrderStatus { get; set; }

    public int ExpertId { get; set; }

    public int BasePrice { get; set; }
    public bool IsDeleted { get; set; }


    public string Discription { get; set; } = null!;

    public DateTime PrefferedDateTime { get; set; }

    public virtual ICollection<Bid> Bids { get; set; } = new List<Bid>();

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<OrderFile> OrderFiles { get; set; } = new List<OrderFile>();

    public virtual ICollection<OrederProposal> OredeProposals { get; set; } = new List<OrederProposal>();

    public virtual Service.Entities.Service Service { get; set; } = null!;
    public virtual Customer Customer { get; set; } = null!;
    public virtual Expert Expert { get; set; } = null!;
}








