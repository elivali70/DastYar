namespace App.EndPoint.UI.Areas.Admin.Models.Order
{
    public class CommentViewModel
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public string? Discription { get; set; }

        public DateTime CreatedAt { get; set; }
        public int UserId { get; set; }
    }
}
