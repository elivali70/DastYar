using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace App.EndPoint.UI.Areas.Customer.Models.UpdateProfile
{
    public class UpdateProfileViewModel
    {
        [ReadOnly(true)]
        [Display(Name = "شناسه کاربر")]
        public int UserId { get; set; }

        [Required]
        [Display(Name = " نام ")]
        public string Name { get; set; }

        [Required]
        [Display(Name = " نام خانوادگی")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = " نام کاربری")]
        public string UserName { get; set; }

        [Display(Name = "ایمیل")]
        [Required]
        [EmailAddress]
        public string Email { get; set; }


        [Required]
        [Display(Name = "شماره تماس")]
        public string? PhoneNumber { get; set; }

        [Display(Name = " فایل")]
        public IFormFile File { get; set; }

        [Display(Name = " نام فایل")]
        public string FileName { get; set; }

        [Display(Name = "توصیح فایل")]
        public string DiscriptionFile { get; set; }

        [Display(Name = " آدرس ")]
        public string Address { get; set; }

    }
}