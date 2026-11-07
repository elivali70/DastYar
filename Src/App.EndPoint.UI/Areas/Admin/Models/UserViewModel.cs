using App.EndPoint.UI.Models.Account;
using System.ComponentModel.DataAnnotations;

namespace App.EndPoint.UI.Areas.Admin.Models
{
    public class UserViewModel
    {
        [Display(Name = "شناسه کاربر")]
        public int Id { get; set; }

        [Required]
        [Display(Name = "نام")]
        public string Name { get; set; }


        [Required(ErrorMessage = "وارد کردن {0} اجباری است.")]
        [Display(Name = "نام و نام خانوادگی ")]
        public string LastName { get; set; }


        [Required(ErrorMessage = "وارد کردن {0} اجباری است.")]
        [Display(Name = "نشانی")]
        public string Address { get; set; }

        [Required(ErrorMessage = "وارد کردن {0} اجباری است.")]
        [Display(Name = "نام کاربری")]
        public string UserName { get; set; }

        [Display(Name = "ایمیل")]
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Display(Name = "شماره تماس")]
        public string PhoneNumber { get; set; }

        [Display(Name = "نقش‌های کاربر")]
        public string Role { get; set; }

        [Display(Name = "قایل")]
        public IFormFile? File{ get; set; }

        [Display(Name = "توصیح فایل")]
        public string DiscriptionFile { get; set; }

    }
}
