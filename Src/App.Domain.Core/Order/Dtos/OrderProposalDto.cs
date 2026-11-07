using App.Domain.Core.Order.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Core.Order.Dtos
{
    public class OrderProposalDto
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public ProposalStatusEnum ProposalStatus { get; set; }

        public DateTime CreatedAt { get; set; }

        public int ExpertId { get; set; }

        public int SuggestedPrice { get; set; }

        public int DurationTime { get; set; }

        public DateTime StatrtedDateTime { get; set; }

        public string? Discription { get; set; }

    }
}
