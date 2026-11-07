using App.Domain.Core.User.Enums;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace App.EndPoint.Mvc.ShopUI.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "وارد کردن {0} اجباری است.")]
        [Display(Name = "نام ")]
        public string Name { get; set; }

        [Required(ErrorMessage = "وارد کردن {0} اجباری است.")]
        [Display(Name = "نام و نام خانوادگی ")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "وارد کردن {0} اجباری است.")]
        [Display(Name = "نام کاربری")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "وارد کردن {0} اجباری است.")]
        [EmailAddress]
        [Display(Name = "ایمیل")]
        public string Email { get; set; }

        [Required(ErrorMessage = "وارد کردن {0} اجباری است.")]
        [StringLength(100, ErrorMessage = "{0} حداقل {1} کاراکتر و حداکتر {2} باشد", MinimumLength = 3)]
        [DataType(DataType.Password)]
        [Display(Name = "رمز عبور")]
        public string Password { get; set; }

        [Display(Name = "تکرار رمز عبور")]
        [Compare(nameof(Password), ErrorMessage = "رمز عبور و تکرار رمز عبور باید یکسان باشند")]
        public string ConfirmPassword { get; set; }

        [Phone(ErrorMessage = "شماره تلفن معتبر نیست.")]
        [Display(Name = "شماره تلفن")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "وارد کردن {0} اجباری است.")]
        [Display(Name = "نشانی")]
        public string Address { get; set; }

      
    }
}
