using App.Domain.Core.Order.Enities;
using App.Domain.Core.Order.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace App.EndPoint.UI.Areas.Expert.Models.Order
{
    public class SuggestedPriceViewModel
    {
        [ReadOnly(true)]
        [Display(Name = "شناسه سفارش")]
        public int OrderId { get; set; }

        [Display(Name = "قیمت پیشنهادی ")]
        [ReadOnly(true)]
        public int SuggestedPrice { get; set; }

        [ReadOnly(true)]
        [Display(Name = "شناسه متخصص")]
        public int ExpertId { get; set; }

        
    }
}
