using App.Domain.Core.Order.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Core.Order.Dtos
{
    public class BidDto
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public int ExpertId { get; set; }

        public DateTime CreatedAt { get; set; }

        public int SuggestedPrice { get; set; }
        public ProposalStatusEnum ProposalStatus { get; set; }

    }
}
