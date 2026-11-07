using App.Domain.Core.Order.Enums;

namespace App.EndPoint.UI.Areas.Admin.Models.Order
{
    public class OrderProposalOutPutViewModel
    {
    public int Id { get; set; }

    public int OrderId { get; set; }

    public ProposalStatusEnum ProposalStatus { get; set; }

    public DateTime CreatedAt { get; set; }

    public string ExpertUserName { get; set; }

    public int SuggestedPrice { get; set; }

    public int DurationTime { get; set; }

    public DateTime StatrtedDateTime { get; set; }

    public string? Discription { get; set; }
    }
}
