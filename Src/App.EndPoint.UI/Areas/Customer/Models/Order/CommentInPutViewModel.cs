using App.Domain.Core.Order.Enities;
using System.ComponentModel;

namespace App.EndPoint.UI.Areas.Customer.Models.Order
{
    public class CommentInPutViewModel
    {
        [ReadOnly(true)]
        public int OrderId { get; set; }

    public string? Discription { get; set; }

    }
}
