using App.Domain.Core.Order.Enums;

public class OpenOrderOutPutDto
{
    public int OrderId { get; set; }

    public string CustomerUserName { get; set; }
    public string CustomerLastName { get; set; }
    public string CustomerPhoneNumber { get; set; }
    public string CustomerAddress { get; set; }

    public string ServiceName { get; set; }

    public DateTime CreatedAt { get; set; }

    public OrderStatusEnum OrderStatus { get; set; }

    public string ExpertUserName { get; set; }

    public int BasePrice { get; set; }


    public string Discription { get; set; } = null!;

    public DateTime PrefferedDateTime { get; set; }


}
