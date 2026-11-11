using App.Domain.Core.Order.Enities;
using App.Domain.Core.Order.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Core.Order.Dtos
{
    public class OrderDto
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }
        public int? ExpertId { get; set; }

        public int ServiceId { get; set; }

        public DateTime CreatedAt { get; set; }

        public OrderStatusEnum OrderStatus { get; set; }

        public int BasePrice { get; set; }

        public string Discription { get; set; } = null!;

        public DateTime PrefferedDateTime { get; set; }

      
    }
}
