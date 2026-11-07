using App.Domain.Core.Order.Enums;
using System.ComponentModel.DataAnnotations;

public class OrderOutPutViewModel
{

    [Display(Name = "شناسه سفارش")]
    public int Id { get; set; }
    [Display(Name = "شماره تلقن مشتری")]
    public string CustomerUserName { get; set; }
    [Display(Name = "نام خدمات")]
    public string ServiceName { get; set; }

    [Display(Name = "وضعیت سفارش")]
    public OrderStatusEnum OrderStatus { get; set; }
    [Display(Name = "نام کاربری متخصص")]
    public string ExpertUserName { get; set; }
    [Display(Name = " قیمت پایه")]
    public int BasePrice { get; set; }
    [Display(Name = "تاریخ ایجاد")]
    public DateTime CreatedAt { get; set; }
    [Display(Name = " توضیحات")]
    public string Discription { get; set; } = null!;
    [Display(Name = "زمان انتخابی انجام کار")]
    public DateTime PrefferedDateTime { get; set; }
}
