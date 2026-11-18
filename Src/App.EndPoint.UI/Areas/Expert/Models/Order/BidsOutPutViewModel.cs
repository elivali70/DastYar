using App.Domain.Core.Order.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace App.EndPoint.UI.Areas.Expert.Models.Order
{
    public class BidsOutPutViewModel
    {
        [ReadOnly(true)]
        [Display(Name = "شناسه ")]
        public int Id { get; set; }

        [Display(Name = "شناسه سفارش")]
        [ReadOnly(true)]
        public int OrderId { get; set; }

        [Display(Name = "شناسه متخصص")]
        [ReadOnly(true)]
        public int ExpertId { get; set; }

        [Display(Name = "تاریخ ایجاد")]
        public DateTime CreatedAt { get; set; }

        [Display(Name = "قیمت پبشنهادی")]
        public int SuggestedPrice { get; set; }

        [Display(Name = "وضعیت پیشنهاد متخصص")]
        public ProposalStatusEnum ProposalStatus { get; set; }
    }
}
