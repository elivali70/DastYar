using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace App.EndPoint.UI.Areas.Expert.Models.Home
{
    public class ExpertFavoriteServiceViewModel
    {
        [ReadOnly(true)]
        [Display(Name = "شناسه کاربر")]
        public int UserId { get; set; }

        [ReadOnly(true)]
        [Display(Name = "شناسه متخصص")]
        public int ExpertId { get; set; }

        [Required]
        [Display(Name = "سرویس‌های مورد علاقه")]
        public List<int> SelectedServiceIds { get; set; } = new();

        [Required]
        [Display(Name = "نام سرویس")]
        public List<SelectListItem> Services { get; set; }= new();
       
    }
}

