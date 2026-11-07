using App.Domain.Core.Order.Enums;
using System.ComponentModel.DataAnnotations;

namespace App.EndPoint.UI.Areas.Admin.Models.Order
{
    public class OrderViewModel
    {
        [Display(Name ="شناسه سفارش")]
        public int Id { get; set; }
        [Display(Name = "شناسه مشتری")]
        public int CustomerId { get; set; }
        [Display(Name = "شناسه خدمات")]
        public int ServiceId { get; set; }

        [Display(Name = "وضعیت سفارش")]
        public OrderStatusEnum OrderStatus { get; set; }
        [Display(Name = "شناسه متخصص")]
        public int ExpertId { get; set; }
        [Display(Name = " قیمت پایه")]
        public int BasePrice { get; set; }
        [Display(Name = "تاریخ ایجاد")]
        public DateTime CreatedAt { get; set; }
        [Display(Name = " توضیحات")]
        public string Discription { get; set; } = null!;
        [Display(Name = "زمان انتخابی انجام کار")]
        public DateTime PrefferedDateTime { get; set; }
    }
}
