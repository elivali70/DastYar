using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace App.EndPoint.UI.Areas.Customer.Models.Order
{
    public class OrderCustomerViewModel
    {
        [Display(Name = "شناسه مشتری")]
        [ReadOnly(true)]
        public int CustomerId { get; set; }

        [Display(Name = "شناسه خدمت")]
        public int ServiceId { get; set; }

        [Display(Name = "توضیخات")]
        public string Discription { get; set; } = null!;

        [Display(Name = "زمان ترجیحی")]
        [DataType(DataType.DateTime)]
        public DateTime PrefferedDateTime { get; set; }

    }
}
