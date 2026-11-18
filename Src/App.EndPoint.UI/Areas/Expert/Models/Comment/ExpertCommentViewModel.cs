using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace App.EndPoint.UI.Areas.Expert.Models.Comment
{
    public class ExpertCommentViewModel
    {
        [ReadOnly(true)]
        [Display(Name = "شناسه سفارش")]
        public int OrderId { get; set; }


        [Display(Name = " ثبت دیدگاه")]
        public string? Discription { get; set; }
    }
}
