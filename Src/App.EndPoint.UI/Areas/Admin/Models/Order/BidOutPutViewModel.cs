using App.Domain.Core.Order.Enums;

namespace App.EndPoint.UI.Areas.Admin.Models.Order
{
    public class BidOutPutViewModel
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public string ExpertUserName { get; set; }

        public DateTime CreatedAt { get; set; }

        public int SuggestedPrice { get; set; }
        public ProposalStatusEnum ProposalStatus { get; set; }
    }
}
