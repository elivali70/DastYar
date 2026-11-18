using App.Domain.Core.Order.Enities;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace App.EndPoint.UI.Areas.Customer.Models.Order
{
    public class CommentInPutViewModel
    {
        [ReadOnly(true)]
        [Display(Name = "شناسه سفارش")]

        public int OrderId { get; set; }

        [Display(Name = " ثبت دیدگاه")]
       public string? Discription { get; set; }

    }
}
