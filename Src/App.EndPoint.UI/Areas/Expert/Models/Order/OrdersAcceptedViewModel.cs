using App.Domain.Core.Order.Enities;
using App.Domain.Core.Order.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace App.EndPoint.UI.Areas.Customer.Models.Order
{
    public class OrdersAcceptedViewModel
    {
        [Display(Name = "شناسه سفارش")]
        public int Id { get; set; }

        [Display(Name = "شناسه سفارش")]
        [ReadOnly(true)]
        public int OrderId { get; set; }

        [Display(Name = "شناسه خدمت")]
        public int ServiceId { get; set; }

        [Display(Name = "نام کاربری متخصص")]
        public string? ExpertUserName { get; set; }

        [Display(Name = "تاریخ ثیت سفارش")]
        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; }

        [Display(Name = "پیشنهاد")]
        [ReadOnly(true)]
        public string Comment { get; set; }

        [Display(Name = "تاریخ ثیت پیشنهاد")]
        public DateTime CommentCreatedTime { get; set; }
    }
}
