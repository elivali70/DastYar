using App.Domain.Core.Order.Enums;
using System;
using System.Collections.Generic;

namespace App.Domain.Core.Order.Enities;

public partial class Bid
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public int ExpertId { get; set; }

    public DateTime CreatedAt { get; set; }

    public int SuggestedPrice { get; set; }

    public bool IsDeleted { get; set; }

    public ProposalStatusEnum ProposalStatus { get; set; }

    public virtual Order Order { get; set; } = null!;
}
