using App.Domain.Core.Order.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

public class OrderOutPutCustomeViewModel
{
    [Display(Name = "شناسه سفارش")]
    public int Id { get; set; }

    [Display(Name = "شناسه مشتری")]
    [ReadOnly(true)]
    public int CustomerId { get; set; }

    [Display(Name = "نام کاربری متخصص")]

    public string? ExpertUserName { get; set; }

    [Display(Name = "تاریخ ثیت سفارش")]
    [DataType(DataType.DateTime)]
    public DateTime CreatedAt { get; set; }

    [Display(Name = "وضعیت سفارش")]
    public OrderStatusEnum OrderStatus { get; set; }

    [Display(Name = "قیمت پایه")]
    public int BasePrice { get; set; }

    [Display(Name = "شناسه خدمت")]
    public int ServiceId { get; set; }

    [Display(Name = "توضیخات")]
    public string Discription { get; set; } = null!;

    [Display(Name = "زمان ترجیحی")]
    [DataType(DataType.DateTime)]
    public DateTime PrefferedDateTime { get; set; }

    [Display(Name = "قیمت پیشنهادی")]
    public int SuggestedPrice { get; set; }

    [Display(Name = "مدت زمان انجام کار ")]
    public int DurationTime { get; set; }

    [Display(Name = " زمان شروع  ")]
    public DateTime StatrtedDateTime { get; set; }

    [Display(Name = " وضعیت پیشنهاد متخصص ")]
    public ProposalStatusEnum ProposalStatus { get; set; }
}
